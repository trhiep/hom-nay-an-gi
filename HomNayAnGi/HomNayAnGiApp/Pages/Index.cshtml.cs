using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using HomNayAnGiApp.Models;
using HomNayAnGiApp.Models.DTO;
using HomNayAnGiApp.Utils.JWTHelper;

namespace HomNayAnGiApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string RecipeDtoUrl = "http://localhost:5000/api/Recipes/get-list-recipe-dto";
        private string UserDtoUrl = "http://localhost:5000/api/Users/";
        private readonly IHttpContextAccessor _contextAccessor;


        [BindProperty]
        public string? LoggedInUsername { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor contextAccessor )
        {
            _contextAccessor = contextAccessor;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public List<RecipeDTO> RandomRecipe { get; set; } = default!;
        public User UserByID { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var accessToken = _contextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            }

            // Gọi API để lấy danh sách các công thức nấu ăn
            HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
            if (response.IsSuccessStatusCode)
            {
                string recipeDtoJSONString = await response.Content.ReadAsStringAsync();
                RandomRecipe = JsonConvert.DeserializeObject<IList<RecipeDTO>>(recipeDtoJSONString).Where(x=>x.IsPublic==1).ToList();
                RandomRecipe = RandomRecipe.OrderBy(x => Guid.NewGuid()).ToList();
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostRandomRecipeAsync()
        {
            return Page();
        }
    }
}
