using HomNayAnGiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace HomNayAnGiApp.Pages.Recipes
{
    public class CategoryModel : PageModel
    {
        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage employSkillList = _httpClient.GetAsync("http://localhost:5000/api/RecipeCategories").Result;
            var employees = employSkillList.Content.ReadFromJsonAsync<List<RecipeCategory>>().Result;
            ViewData["recipeCate"] = employees;
        }

        public async Task<IActionResult> OnPostAddAsync(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "http://localhost:5000/api/RecipeCategories"; // Replace with the actual category ID

                    var updatedCategory = new
                    {
                        categoryName = name  // Replace with the new name
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
