using HomNayAnGiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomNayAnGiApp.Pages.UsersPage
{
    public class ListModel : PageModel
    {
        public void OnGet()
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage employSkillList = _httpClient.GetAsync("http://localhost:5000/api/Users").Result;
            var employees = employSkillList.Content.ReadFromJsonAsync<List<User>>().Result;
            ViewData["API"] = employees;
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {

            HttpClient _httpClient = new HttpClient();
            var content = new StringContent("");
            HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/Users/{id}");
            return RedirectToAction(nameof(OnGet));
        }
    }
}
