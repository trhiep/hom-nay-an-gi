using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Authorization;

namespace HomNayAnGiApp.Pages.UserFavorites
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;//this is for get username or id logined
        private string RecipeDtoUrl = "http://localhost:5000/api/UserFavorites/get-all-my-recipes";

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<UserFavoriteDTO> UserFavorites { get; set; } = new List<UserFavoriteDTO>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchBy { get; set; } // "name" or "category"

        [BindProperty]
        public string? LoggedInUsername { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //----------------------------------------------------------------------------------
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine(accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login/Index");
            }
            var LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            var LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
            //----------------------------------------------------------------------------------
            HttpResponseMessage response = await _httpClient.GetAsync($"{RecipeDtoUrl}/{LoggedInUsername}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                UserFavorites = JsonConvert.DeserializeObject<IList<UserFavoriteDTO>>(jsonString).ToList();
            }

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                SearchTerm = SearchTerm.ToLower().Trim();
                if (SearchBy == "name")
                {
                    UserFavorites = UserFavorites.Where(r => r.RecipeName.ToLower().Contains(SearchTerm)).ToList();
                }
            }
            return Page();
        }
    }
}
