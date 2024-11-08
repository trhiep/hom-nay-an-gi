using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HomNayAnGiApp.Utils.JWTHelper
{
    public class JwtHelper
    {
        public static void ReadJwtClaims(string jwtToken)
        {
            try
            {
                // Phân tích JWT
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    // Lấy các claims
                    foreach (var claim in jsonToken.Claims)
                    {
                        Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("Token không hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc JWT: {ex.Message}");
            }
        }

        public static string GetUsernameFromClaims(string jwtToken)
        {
            try
            {
                // Phân tích JWT
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    return jsonToken?.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
                }
                else
                {
                    Console.WriteLine("Invalid token.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when read JWT: {ex.Message}");
            }
            return "";
        }

        public static string GetUserIdFromClaims(string jwtToken)
        {
            try
            {
                // Phân tích JWT
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    return jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                }
                else
                {
                    Console.WriteLine("Invalid token.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when read JWT: {ex.Message}");
            }
            return "";
        }
        public static string GetRoleFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            // Retrieve the role claim (or use ClaimTypes.Role if your JWT uses standard claims)
            var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role" || claim.Type == ClaimTypes.Role);

            return roleClaim?.Value;
        }
    }
}
