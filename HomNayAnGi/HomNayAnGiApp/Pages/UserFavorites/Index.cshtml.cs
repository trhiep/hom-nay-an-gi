using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HomNayAnGiApp.Models.DTO;

namespace HomNayAnGiApp.Pages.UserFavorites
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string RecipeDtoUrl = "http://localhost:5000/api/UserFavorites/get-all-my-recipes/tranhiep";//đang fix cứng username người đăng nhập

        public IndexModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<UserFavoriteDTO> UserFavorites { get; set; } = new List<UserFavoriteDTO>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchBy { get; set; } // "name" or "category"

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
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
        }
    }
}
