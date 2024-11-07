using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using HomNayAnGiApp.Models;
using HomNayAnGiApp.Models.DTO;

namespace HomNayAnGiApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string RecipeDtoUrl = "http://localhost:5000/api/Recipes/get-list-recipe-dto";
        private string UserDtoUrl = "http://localhost:5000/api/Users/";


        public IndexModel(ILogger<IndexModel> logger)
        {

            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public List<RecipeDTO> RandomRecipe { get; set; } = default!;
        public User UserByID { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            // Gọi API để lấy danh sách các công thức nấu ăn
            HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
            if (response.IsSuccessStatusCode)
            {
                string recipeDtoJSONString = await response.Content.ReadAsStringAsync();
                RandomRecipe = JsonConvert.DeserializeObject<IList<RecipeDTO>>(recipeDtoJSONString).Where(x=>x.IsPublic==1).ToList();

                // Chọn ngẫu nhiên một công thức từ danh sách
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostRandomRecipeAsync()
        {
            //// Gọi API để lấy danh sách các công thức nấu ăn
            //HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
            //if (response.IsSuccessStatusCode)
            //{
            //    string filmsJSONString = await response.Content.ReadAsStringAsync();
            //    var recipes = JsonConvert.DeserializeObject<IList<RecipeDTO>>(filmsJSONString).Where(x=>x.IsPublic==1).ToList();

            //    // Chọn ngẫu nhiên một công thức từ danh sách
            //    if (recipes != null && recipes.Count > 0)
            //    {
            //        var random = new Random();
            //        int index = random.Next(recipes.Count);
            //        RandomRecipe = recipes[index];
            //    }
            //}
            //HttpResponseMessage responseUser = await _httpClient.GetAsync(UserDtoUrl + RandomRecipe.UserId);
            //if (responseUser.IsSuccessStatusCode)
            //{
            //    string UserJSONString = await responseUser.Content.ReadAsStringAsync();
            //    UserByID = JsonConvert.DeserializeObject<User>(UserJSONString);
            //}
            return Page();
        }
    }
}
