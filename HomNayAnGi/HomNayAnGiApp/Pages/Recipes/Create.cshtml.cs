using HomNayAnGiApp.Models;
using HomNayAnGiApp.Models.DTO;
using HomNayAnGiApp.Utils.JWTHelper;
using HomNayAnGiAPP.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string RecipesUrl = "http://localhost:5000/api/Recipes";
        private readonly string CategoriesUrl = "http://localhost:5000/api/RecipeCategories";

        public CreateModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public RecipeCreateRequest RecipeCreateRequestModel { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine(accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login/Index");
            }

            var CategoryId = JwtHelper.GetUsernameFromClaims(accessToken);
            HttpResponseMessage responseCategories = await _httpClient.GetAsync($"{CategoriesUrl}/user/{CategoryId}");
            if (responseCategories.IsSuccessStatusCode)
            {
                string jsonString = await responseCategories.Content.ReadAsStringAsync();
                ViewData["CategoryId"] =
                    new SelectList(JsonConvert.DeserializeObject<IList<RecipeCategoryDTO>>(jsonString), "CategoryId", "CategoryName");
            }

            return Page();
        }
    }
}
