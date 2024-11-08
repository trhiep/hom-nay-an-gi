using HomNayAnGiApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using static HomNayAnGiApp.Pages.Recipes.CategoryModel;

namespace HomNayAnGiApp.Pages.Recipes
{
    [Authorize]
    public class EditCategoryModel : PageModel
    {
     
        public void OnGet(int id)
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage employSkillList = _httpClient.GetAsync($"http://localhost:5000/api/RecipeCategories/{id}").Result;
            var employees = employSkillList.Content.ReadFromJsonAsync<RecipeCategory>().Result;
            ViewData["editCate"] = employees;
        }

        public async Task<IActionResult> OnPostEditAsync(string name, int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"http://localhost:5000/api/RecipeCategories/{id}"; // Replace with the actual category ID

                    var updatedCategory = new
                    {
                        categoryId = id,  // Replace with the ID of the category to update
                        categoryName = name  // Replace with the new name
                    };

                    string jsonContent = JsonSerializer.Serialize(updatedCategory);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Category updated successfully.");
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode}");
                        Console.WriteLine($"Response Body: {responseBody}");
                    }
                    return RedirectToPage("/Recipes/Category");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                    // return RedirectToPage("/Recipes/Category");
                    return Page();
                }
            }
        }
    }
}
