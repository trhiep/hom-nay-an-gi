using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Signup
{
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        private readonly string SignupUrl = "http://localhost:5000/api/Authentication/signup";

        public IndexModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public SignupViewModel SignupViewModel { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                SignupRequestDto signupRequestDto = new SignupRequestDto()
                {
                    Username = SignupViewModel.Username,
                    Password = SignupViewModel.Password,
                    ConfirmPassword = SignupViewModel.ConfirmPassword,
                    Email = SignupViewModel.Email,
                    SignupRequestId = Guid.NewGuid().ToString()
                };
                string jsonStr = JsonConvert.SerializeObject(signupRequestDto);
                var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(SignupUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic tempData = JObject.Parse(responseContent);
                    var signupResponse = JsonConvert.DeserializeObject<SignupResponse>(tempData.ToString());
                    if (signupResponse != null)
                    {
                        if (signupResponse.InvalidFields == null)
                        {
                            HttpContext.Session.SetString("SignupRequest", JsonConvert.SerializeObject(signupRequestDto));
                            return RedirectToPage("/Signup/VerifyOTP");
                        }
                    }
                }
                ViewData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình xử lý. Vui lòng thử lại sau";
            }
            return Page();
        }
    }
}
