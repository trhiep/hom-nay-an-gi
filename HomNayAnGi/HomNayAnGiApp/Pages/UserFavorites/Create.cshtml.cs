using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using HomNayAnGiApp.Utils.JWTHelper;
using Newtonsoft.Json;

namespace HomNayAnGiApp.Pages.UserFavorites
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string UserFavoriteUrl = "http://localhost:5000/api/UserFavorites";

        public CreateModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGet(int recipeId)
        {
            return await OnPostAsync(recipeId.ToString());
        }

        public async Task<IActionResult> OnPostAsync(string recipeId)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login/Index");
            }

            var LoggedInUserId = JwtHelper.GetUserIdFromClaims(accessToken);
            var LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);

            // Check for duplicate
            HttpResponseMessage res = await _httpClient.GetAsync($"{UserFavoriteUrl}/{LoggedInUsername}/{recipeId}");
            if (res.IsSuccessStatusCode && !string.IsNullOrEmpty(await res.Content.ReadAsStringAsync()))
            {
                // Duplicate found, redirecting
                return RedirectToPage("/UserFavorites/Index");
            }

            // Add new recipe to favorites
            var userFavoriteDTO = new
            {
                userId = LoggedInUserId,
                recipeId = recipeId
            };

            var jsonStr = JsonConvert.SerializeObject(userFavoriteDTO);
            var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(UserFavoriteUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/UserFavorites/Index");
            }

            return RedirectToPage("/Recipes/List");
        }
    }
}
