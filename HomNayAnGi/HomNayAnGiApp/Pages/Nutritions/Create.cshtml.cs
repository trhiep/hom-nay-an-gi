using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HomNayAnGiApp.Pages.Nutritions
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string NutritionUrl = "http://localhost:5000/api/NutritionFacts";

        public CreateModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }
        [BindProperty]
        public string? LoggedInUsername { get; set; }

        [BindProperty]
        public NutritionFactDTO NutritionFactDTO { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }

        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id == 0)
            {
                return RedirectToPage("/Index");
            }
            RecipeId = Id;
            await Console.Out.WriteLineAsync("RECIPE: " + RecipeId);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NutritionFactDTO.RecipeId = RecipeId; // Gán RecipeId cho NutritionFactDTO
            var jsonContent = JsonConvert.SerializeObject(NutritionFactDTO);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(NutritionUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage($"/Recipes/Update", new { Id = RecipeId });
            }

            ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu dinh dưỡng.");
            return Page();
        }

    }
}
