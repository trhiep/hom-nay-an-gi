using HomNayAnGiAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace HomNayAnGiApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string RecipeDtoUrl = "http://localhost:5000/api/Recipes/get-list-recipe-dto";

        public IndexModel(ILogger<IndexModel> logger)
        {

            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public RecipeDTO RandomRecipe { get; set; } = default!;

        public async Task<IActionResult> OnPostRandomRecipeAsync()
        {
            // Gọi API để lấy danh sách các công thức nấu ăn
            HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
            if (response.IsSuccessStatusCode)
            {
                string filmsJSONString = await response.Content.ReadAsStringAsync();
                var recipes = JsonConvert.DeserializeObject<IList<RecipeDTO>>(filmsJSONString);

                // Chọn ngẫu nhiên một công thức từ danh sách
                if (recipes != null && recipes.Count > 0)
                {
                    var random = new Random();
                    int index = random.Next(recipes.Count);
                    RandomRecipe = recipes[index];
                }
            }

            return Page();
        }
    }
}
