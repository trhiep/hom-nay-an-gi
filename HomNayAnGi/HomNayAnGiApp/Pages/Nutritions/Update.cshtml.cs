using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Nutritions
{
    [Authorize]
    public class UpdateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string NutritionUrl = "http://localhost:5000/api/NutritionFacts";

        public UpdateModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public NutritionFactDTO NutritionFactDTO { get; set; }

        public int RecipeId { get; set; }

        // Lấy dữ liệu dinh dưỡng của món ăn khi truy cập vào trang cập nhật
        public async Task<IActionResult> OnGetAsync(int recipeId)
        {
            RecipeId = recipeId;

            // Gửi yêu cầu GET để lấy giá trị dinh dưỡng cho món ăn từ API
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
                return RedirectToPage("/Nutritions/Index");
            }

            return Page();
        }

        // Xử lý cập nhật dữ liệu khi người dùng nhấn "Cập nhật"
        public async Task<IActionResult> OnPostAsync(int recipeId)
        {
			RecipeId = recipeId;
			// Kiểm tra tính hợp lệ của Model
			if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cập nhật dữ liệu dinh dưỡng
            var jsonContent = JsonConvert.SerializeObject(NutritionFactDTO);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Gửi yêu cầu PUT đến API để cập nhật dữ liệu
            var response = await _httpClient.PutAsync($"{NutritionUrl}/{RecipeId}", content);

            if (response.IsSuccessStatusCode)
            {
                // Chuyển hướng về trang Index sau khi cập nhật thành công
                return RedirectToPage($"/Nutritions/Index", new { Id = recipeId });
            }

            // Nếu có lỗi khi cập nhật, thêm lỗi vào ModelState
            ModelState.AddModelError(string.Empty, "Lỗi khi cập nhật giá trị dinh dưỡng.");
            return Page();
        }   
    }
}
