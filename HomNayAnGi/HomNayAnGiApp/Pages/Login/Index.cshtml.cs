using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Login
{
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        private readonly string LoginUrl = "http://localhost:5000/api/Authentication/login";

        public IndexModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IActionResult OnGet()
        {
            Response.Cookies.Delete("accessToken");
            Request.Cookies.TryGetValue("accessToken", out var accessToken);
            if (accessToken != null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        [BindProperty]
        public LoginRequestDto LoginRequestDto { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string jsonStr = JsonConvert.SerializeObject(LoginRequestDto);
                var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(LoginUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseJsonString = await response.Content.ReadAsStringAsync();
                    dynamic tempResData = JObject.Parse(responseJsonString);
                    string accessToken = tempResData.accessToken;
                    string refreshToken = tempResData.refreshToken;

                    if (accessToken != null && refreshToken != null)
                    {
                        Response.Cookies.Append("accessToken", accessToken, new CookieOptions
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict
                        });

                        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict
                        });
                    }

                    return RedirectToPage("./Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Tên tài khoản hoặc mật khẩu không chính xác";
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Vui lòng điền đầy đủ thông tin hợp lệ.";
            }
            return Page();
        }
    }
}
