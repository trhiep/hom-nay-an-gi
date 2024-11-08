using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HomNayAnGiApp.Pages.Nutritions
{
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
        public NutritionFactDTO NutritionFactDTO { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //// Lấy token từ cookie
            //var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    return RedirectToPage("/Login/Index");
            //}

            //// Gán token vào header của HttpClient
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //// Thực hiện các logic khác cho OnGet nếu cần, ví dụ: lấy danh mục giá trị dinh dưỡng từ API khác (nếu có)

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NutritionFactDTO.RecipeId = 47;
            // Serialize NutritionFactDTO thành JSON
            var jsonContent = JsonConvert.SerializeObject(NutritionFactDTO);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Gửi yêu cầu POST đến API Nutrition
            var response = await _httpClient.PostAsync(NutritionUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage($"/Recipes/Details?id={NutritionFactDTO.RecipeId}"); // Chuyển hướng về trang Index nếu thành công
                //return Page();
            }

            // Nếu có lỗi khi gửi yêu cầu, hiển thị thông báo lỗi
            ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu dinh dưỡng.");
            return Page();
        }
    }
}
