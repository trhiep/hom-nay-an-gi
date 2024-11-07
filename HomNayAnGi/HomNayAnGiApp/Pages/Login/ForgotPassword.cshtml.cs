using HomNayAnGiApp.Models;
using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HomNayAnGiApp.Pages.Login
{
    public class ForgotPasswordModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPost(string username)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                HttpResponseMessage response = _httpClient.GetAsync("http://localhost:5000/api/Users/get-by-username/" + username).Result;
                var user = response.Content.ReadFromJsonAsync<User>().Result;
                if (response.IsSuccessStatusCode)
                {
                            HttpContext.Session.SetString("SignupRequest", JsonConvert.SerializeObject(signupRequestDto));
                            return RedirectToPage("/Signup/VerifyOTP");
                }
                return Page();
            }catch(Exception e)
            {
                return Page();
            }
            
        }
    }
}
