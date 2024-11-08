using HomNayAnGiApp.Models.APIModel;
using HomNayAnGiApp.Models.DTO;
using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace HomNayAnGiApp.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string RecipeUrl = "http://localhost:5000/api/Recipes";

        public DetailsModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            //Retrieve the token from cookies and set it as the Authorization header
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine("SAVED TOKEN = " + accessToken);
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }

        [BindProperty]
        public RecipeDTO RecipeDTO { get; set; }

        [BindProperty]
        public string UsernameLogin { get; set; }//for comment

        [BindProperty]
        public int UserIdLogin { get; set; }//for comment

        public async Task<IActionResult> OnGet(int? Id)
        {
            await Console.Out.WriteLineAsync("ID IS:" + Id);
            if (Id != null)
            {
                await Console.Out.WriteLineAsync(_httpClient.DefaultRequestHeaders.ToString());
                HttpResponseMessage response = await _httpClient.GetAsync($"{RecipeUrl}/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseJsonString = await response.Content.ReadAsStringAsync();
                    var recipeDtoApiResponse = JsonConvert.DeserializeObject<ApiResponse<RecipeDTO>>(responseJsonString);
                    if (recipeDtoApiResponse != null)
                    {
                        RecipeDTO = recipeDtoApiResponse.Data;                       
                    }
                }
                //----------------------------------for-comment------------------------------------------------
                var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
                Console.WriteLine(accessToken);
                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToPage("/Login/Index");
                }
                var LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
                var LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
                UsernameLogin = LoggedInUsername;
                UserIdLogin = LoggedInUserId;
                //----------------------------------------------------------------------------------

                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
