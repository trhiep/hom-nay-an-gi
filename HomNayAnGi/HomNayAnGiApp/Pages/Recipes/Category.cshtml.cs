using HomNayAnGiApp.Models;
using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace HomNayAnGiApp.Pages.Recipes
{
    [Authorize]
    public class CategoryModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public List<Category> Categories { get; set; }

        [BindProperty]
        public int LoggedInUserId { get; set; }
        [BindProperty]
        public string LoggedInUsername { get; set; }

        public IActionResult OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (accessToken == null)
            {
                return RedirectToPage("/Login/Index");
            }
            Console.WriteLine(accessToken);
            LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
            HttpResponseMessage employSkillList = _httpClient.GetAsync("http://localhost:5000/api/RecipeCategories").Result;
            var employees = employSkillList.Content.ReadFromJsonAsync<List<RecipeCategory>>().Result;
            ViewData["recipeCate"] = employees.Where(x => x.CreatedBy == LoggedInUserId || x.CreatedBy == null).ToList();
            //ViewData["recipeCate"] = employees;
            LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            ViewData["name"] = LoggedInUsername;
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(string name)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine(accessToken);
            LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "http://localhost:5000/api/RecipeCategories"; // Replace with the actual category ID

                    var updatedCategory = new RecipeCategory
                    {
                        CategoryName = name,
                        CreatedBy = LoggedInUserId
                        // Replace with the new name
                    };

                    string jsonContent = JsonSerializer.Serialize(updatedCategory);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Category created successfully.");
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode}");
                        Console.WriteLine($"Response Body: {responseBody}");
                    }
                    return RedirectToAction(nameof(OnGet));
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                    // return RedirectToPage("/Recipes/Category");
                    return RedirectToAction(nameof(OnGet));
                }
            }
        }

        public IActionResult OnPostUpdate(int id)
        {
            // Xử lý cập nhật danh mục
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            HttpClient _httpClient = new HttpClient();
            var content = new StringContent("");
            HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/RecipeCategories/{id}");
            return RedirectToAction(nameof(OnGet));
        }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatedBy { get; set; }
        }
    }
}