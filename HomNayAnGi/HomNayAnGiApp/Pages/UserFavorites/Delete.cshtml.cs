using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HomNayAnGiApp.Pages.UserFavorites
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var url = $"http://localhost:5000/api/UserFavorites/delete/{Username}/{RecipeId}";
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                ViewData["SuccessMessage"] = "Công thức đã được xóa thành công.";
            }
            else
            {
                ViewData["ErrorMessage"] = "Đã có lỗi xảy ra khi xóa công thức.";
            }

            return RedirectToPage("/UserFavorites/Index");
        }
    }

}
