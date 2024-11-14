using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Nutritions
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string NutritionUrl = "http://localhost:5000/api/NutritionFacts";

        public IndexModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }
        [BindProperty]
        public string? LoggedInUsername { get; set; }

        [BindProperty]
        public NutritionFactDTO NutritionFactDTO { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }

        // Lấy dữ liệu dinh dưỡng của món ăn từ API
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            RecipeId = Id;
            // Thực hiện yêu cầu GET đến API để lấy giá trị dinh dưỡng cho món ăn cụ thể (theo RecipeId)
            var response = await _httpClient.GetAsync($"{NutritionUrl}/{RecipeId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var existingNutrition = JsonConvert.DeserializeObject<NutritionFactDTO>(jsonResponse);
                if (existingNutrition != null)
                {
                    // Nếu có dữ liệu, gán vào DTO để hiển thị
                    NutritionFactDTO = existingNutrition;
                }
            }
            else
            {
                // Thêm lỗi vào ModelState nếu không thể tải dữ liệu từ API
                ModelState.AddModelError(string.Empty, "Không thể tải giá trị dinh dưỡng.");
            }

            return Page();
        }

        // Xử lý việc xóa giá trị dinh dưỡng
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await Console.Out.WriteLineAsync("DELETE RECIPE: " +  RecipeId);
            // Gửi yêu cầu DELETE đến API để xóa giá trị dinh dưỡng cho món ăn (dựa trên RecipeId)
            var response = await _httpClient.DeleteAsync($"{NutritionUrl}/{RecipeId}");

            if (response.IsSuccessStatusCode)
            {
                // Chuyển hướng về trang Index sau khi xóa thành công
                return RedirectToPage("/Nutritions/Index");
            }

            // Nếu có lỗi khi xóa, thêm lỗi vào ModelState
            ModelState.AddModelError(string.Empty, "Lỗi khi xóa giá trị dinh dưỡng.");
            return Page();
        }

       
    }
}
