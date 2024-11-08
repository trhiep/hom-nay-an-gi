using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
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
using System.Text.Json.Serialization;

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

        [BindProperty]
        public string LoggedInUsername { get; set; }

        [BindProperty]
        public int LoggedInUserId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine(accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login/Index");
            }
            LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));

            HttpResponseMessage responseCategories = await _httpClient.GetAsync($"{CategoriesUrl}/user/{LoggedInUsername}");
            if (responseCategories.IsSuccessStatusCode)
            {
                string jsonString = await responseCategories.Content.ReadAsStringAsync();
                ViewData["CategoryId"] =
                    new SelectList(JsonConvert.DeserializeObject<IList<RecipeCategoryDTO>>(jsonString), "CategoryId", "CategoryName");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string jsonStr = JsonConvert.SerializeObject(RecipeCreateRequestModel);
            var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");

            string contentString = await content.ReadAsStringAsync();
            Console.WriteLine(contentString);

            HttpResponseMessage response = await _httpClient.PostAsync(RecipesUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "Không có tệp nào được chọn." });
            }

            // Upload ảnh lên Cloudinary
            var cloudinary = new Cloudinary(new Account("dpnvzshsh", "785631258213433", "VR3-KnUWxuPyGs2kVTFLtsrF8mY"));
            string folderPath = "HomNayAnGi";
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folderPath
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new JsonResult(new { secure_url = uploadResult.SecureUri.ToString() });
            }

            return BadRequest(new { error = "Tải ảnh lên thất bại" });
        }

    }
}
