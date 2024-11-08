using HomNayAnGiApp.Models.APIModel;
using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Login
{
    public class VerifyOTPModel : PageModel
    {
        private HttpClient _httpClient;
        private readonly string VerifyOtpUrl = "http://localhost:5000/api/Authentication/verifyOtp";
        public VerifyOTPModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        SignupRequestDto signupRequestDto { get; set; }
        public IActionResult OnGet()
        {
            GetSignupRequestFromSession();
            if (signupRequestDto != null) return Page();
            return RedirectToPage("/Signup/Index");
        }

        private void GetSignupRequestFromSession()
        {
            var SignupRequestInfInSession = HttpContext.Session.GetString("SignupRequest");
            if (SignupRequestInfInSession != null)
            {
                signupRequestDto = JsonConvert.DeserializeObject<SignupRequestDto>(SignupRequestInfInSession);
            }
        }

        [Required(ErrorMessage = "Vui lòng nhập mã OTP.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP Phải có 6 ký tự.")]
        [BindProperty]
        public string EnteredOtp { get; set; }

        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                GetSignupRequestFromSession();
                UserSignupData userSignupData = new UserSignupData()
                {
                    Email = signupRequestDto.Email,
                    Username = signupRequestDto.Username,
                    Password = signupRequestDto.Password,
                    SignupRequestId = signupRequestDto.SignupRequestId
                };
                string jsonStr = JsonConvert.SerializeObject(userSignupData);
                var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{VerifyOtpUrl}/{EnteredOtp}", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic tempData = JObject.Parse(responseContent);
                    ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(tempData.ToString());
                    if (apiResponse.StatusCode == 400)
                    {
                        ErrorMessage = apiResponse.Message;
                    }
                    else if (apiResponse.StatusCode == 401)
                    {
                        return RedirectToPage("/Login/Index", new { ErrorMessage = apiResponse.Message });
                    }
                    else if (apiResponse.StatusCode == 201)
                    {
                        return RedirectToPage("/Login/Index", new { SuccessMessage = apiResponse.Message});
                    }
                    return Page();
                }
                ViewData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình xử lý. Vui lòng thử lại sau";
            }
            return Page();
        }
    }
}
