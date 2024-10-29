using HomNayAnGiAPI.Messages;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.APIModel;
using HomNayAnGiAPI.Models.DTO;
using HomNayAnGiAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly HomNayAnGiContext _context;
        public AuthenticationController(IConfiguration configuration, HomNayAnGiContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            // Lấy ra thông tin người dùng đã submit lên API
            if (loginModel != null && loginModel.UsernameOrEmail != null && loginModel.Password != null)
            {
                
                var user = await GetUser(loginModel.UsernameOrEmail, loginModel.Password);

                if (user != null)
                {
                    string accessToken = JwtHelper.GenerateJwtToken(_configuration, user);
                    string refreshToken = JwtHelper.GenerateRefreshToken();

                    var userToken = new UserRefreshToken()
                    {
                        UserId = user.UserId,
                        RefreshToken = refreshToken,
                        ExpiresAt = DateTime.UtcNow.AddMonths(1),
                        DeviceId = loginModel.DeviceId ?? null,
                    };

                    await _context.UserRefreshTokens.AddAsync(userToken);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    });
                }
                else
                {
                    return BadRequest(MessageSender.Error(ErrorMessage.LoginFailed));
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            // Check is valid request data
            if (signupRequest != null
                && signupRequest.Username != null
                && signupRequest.Password != null
                && signupRequest.Email != null
                && signupRequest.Password != null
                && signupRequest.SignupRequestId != null)
            {

                // Create response data with error (if exists)
                var signupResponse = new SignupResponse()
                {
                    SignupRequestId = signupRequest.SignupRequestId
                };

                // Check for existing email
                var invalidFields = new List<string>();
                if (await IsThisEmailExisted(signupRequest.Email))
                {
                    invalidFields.Add("email");
                }

                // Check for existing username
                if (await IsThisUsernameExisted(signupRequest.Username))
                {
                    invalidFields.Add("username");
                }

                // If there are any errors, return them with the invalid fields
                if (invalidFields.Count > 0)
                {
                    signupResponse.InvalidFields = invalidFields;
                    return Ok(signupResponse);
                }

                // If it's ok, generate an otp
                var signupOtp = new SignupOtp()
                {
                    SignupRequestId = signupRequest.SignupRequestId,
                    Otpstring = OtpHelper.GenerateRandomDigits(6),
                    RequestAttemptsRemains = 3,
                    ExpiresAt = DateTime.Now.AddMinutes(10)
                };
                
                // Save to database
                await _context.SignupOtps.AddAsync(signupOtp);
                await _context.SaveChangesAsync();
                
                // Send otp to user
                // var t = new Thread(() => EmailHelper.SendEmailMultiThread(signupRequest.Email
                //     , "Mã OTP xác minh đăng ký tài khoản"
                //     , $"Xin chào {signupRequest.Username}, đây là mã OTP xác minh đăng ký tài khoản tại Hôm nay ăn gì của bạn: <strong>{signupOtp.OtpString}</strong>."));
                // t.Start();
                
                return Ok(signupResponse);
            }
            return BadRequest();
        }

        [HttpPost("verifyOtp/{enteredOtp}")]
        public async Task<ApiResponse<string>> VerifyOtp( string enteredOtp, UserSignupData userSignupData)
        {
            Console.WriteLine("Entered OTP: " + enteredOtp);
            Console.WriteLine("Data: " + userSignupData.ToString());
            
            // Get this signup request
            var signupRequestOtp = await _context.SignupOtps.FindAsync(userSignupData.SignupRequestId);
            if (signupRequestOtp != null)
            {
                Console.WriteLine("SignupOTP: " + signupRequestOtp.ToString());
                if (signupRequestOtp.Otpstring.Equals(enteredOtp))
                {
                    // Check is this otp expired
                    if (signupRequestOtp.ExpiresAt < DateTime.Now)
                    {
                        return new ApiResponse<string>(401, ErrorMessage.ExpiredOTP);
                    }
                    
                    if (signupRequestOtp.RequestAttemptsRemains <= 0)
                    {
                        await RemoveThisSignupOtp(signupRequestOtp);
                        return new ApiResponse<string>(401, ErrorMessage.OTPValidationFailed);
                    }
                    
                    // Save new user to database
                    var newUser = new User()
                    {
                        Username = userSignupData.Username,
                        Email = userSignupData.Email,
                        Password = PasswordHelper.HashPasswordSHA256(userSignupData.Password),
                        CreatedAt = DateTime.UtcNow,
                        Role = "USER",
                        IsActive = true
                    };
                    await _context.Users.AddAsync(newUser);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        await RemoveThisSignupOtp(signupRequestOtp);
                        return new ApiResponse<string>(201, SuccessMessage.SignupSuccess);
                    }
                }
                
                // Get remain attempts
                var remainAttempts = --signupRequestOtp.RequestAttemptsRemains;
                signupRequestOtp.RequestAttemptsRemains = remainAttempts;
                // Update remain attempts
                _context.SignupOtps.Update(signupRequestOtp);
                await _context.SaveChangesAsync();
                
                if (remainAttempts == 0)
                {
                    await RemoveThisSignupOtp(signupRequestOtp);
                    return new ApiResponse<string>(401, ErrorMessage.OTPValidationFailed);
                }

                return new ApiResponse<string>(400, "Mã OTP không chính xác, bạn còn lại " + remainAttempts + " lần thử.");

            }

            return new ApiResponse<string>(401, ErrorMessage.InvalidRequestId);
        }

        private async Task RemoveThisSignupOtp(SignupOtp signupOtp)
        {
            _context.SignupOtps.Remove(signupOtp);
            await _context.SaveChangesAsync();
        }

        private async Task<User> GetUser(string userNameOrEmail, string password)
        {
            return await _context.Users
                .Where(u => (u.Username.ToLower() == userNameOrEmail.ToLower() || u.Email.ToLower() == userNameOrEmail.ToLower())
                            && u.Password == password)
                .FirstOrDefaultAsync();
        }

        private async Task<bool> IsThisUsernameExisted(string username)
        {
            var user = await _context.Users
                .Where(x => x.Username.ToLower().Equals(username.ToLower()))
                .FirstOrDefaultAsync();
            if (user != null) return true;
            return false;

        }

        private async Task<bool> IsThisEmailExisted(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email.ToLower().Equals(email.ToLower()))
                .FirstOrDefaultAsync();
            if (user != null) return true;
            return false;
        }
    }
}
