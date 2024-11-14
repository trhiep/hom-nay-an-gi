using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IHttpContextAccessor _httpContextAccessor;//this is for get username or id logined

        public DeleteModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }

        [BindProperty]
        public string? LoggedInUsername { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //----------------------------------------------------------------------------------
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine(accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login/Index");
            }
            var LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            var LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
            //----------------------------------------------------------------------------------
            Username = LoggedInUsername;
            var url = $"http://localhost:5000/api/UserFavorites/delete/{Username}/{RecipeId}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);

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
