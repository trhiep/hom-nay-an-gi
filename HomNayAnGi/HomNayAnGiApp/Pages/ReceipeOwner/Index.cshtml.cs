using HomNayAnGiApp.Models.DTO;
using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.ReceipeOwner
{
    [Authorize]
    public class IndexModel : PageModel
    {
		private readonly HttpClient _httpClient;
		private string RecipeDtoUrl = "http://localhost:5000/api/Recipes/get-list-recipe-dto";
		private string RecipeUrl = "http://localhost:5000/api/Recipes";
		private string UserDtoUrl = "http://localhost:5000/api/Users/";



		private readonly IHttpContextAccessor _httpContextAccessor;

		public IndexModel(HomNayAnGiApp.Models.HomNayAnGiContext context, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			_httpClient.DefaultRequestHeaders.Accept.Add(contentType);

			_httpContextAccessor = httpContextAccessor;

			var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
			CurrentUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));

			string isAd = JwtHelper.GetRoleFromJwt(accessToken);

			IsAdmin = isAd.Equals("USER") ? false : true;
		}

		public IList<RecipeDTO> Recipe { get; set; } = default!;

		[BindProperty(SupportsGet = true)]
		public string SearchTerm { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SearchBy { get; set; } // "name" or "category"


		[BindProperty(SupportsGet = true)]
		public bool MyRecipes { get; set; } = false;

		public int CurrentUserId { get; set; }

		public bool IsAdmin { get; set; }

		public async Task OnGetAsync()
		{


			HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
			if (response.IsSuccessStatusCode)
			{
				string filmsJSONString = await response.Content.ReadAsStringAsync();
				Recipe = JsonConvert.DeserializeObject<IList<RecipeDTO>>(filmsJSONString).Where(r => r.UserId == CurrentUserId).ToList();
	
			}
			if (!string.IsNullOrEmpty(SearchTerm))
			{
				if (SearchBy == "name")
				{
					Recipe = Recipe.Where(r => r.RecipeName.ToLower().Contains(SearchTerm.ToLower().Trim())).ToList();
				}
				if (SearchBy == "category")
				{
					Recipe = Recipe.Where(r => r.CategoryName.ToLower().Contains(SearchTerm.ToLower())).ToList();
				}
			}
		}

		public async Task<IActionResult> OnPostDeleteRecipeAsync(int id)
		{


			// Gọi API xóa công thức theo id
			var responseRecipeUr = await _httpClient.DeleteAsync($"{RecipeUrl}/{id}");

			if (responseRecipeUr.IsSuccessStatusCode)
			{

				//string filmsJSONString = await response.Content.ReadAsStringAsync();
				//Recipe = JsonConvert.DeserializeObject<IList<RecipeDTO>>(filmsJSONString).Where(x => x.IsPublic == 1).ToList();
				// Xóa thành công, chuyển hướng về lại trang danh sách
				return RedirectToPage();
			}

			// Xóa thất bại, vẫn ở lại trang và có thể hiển thị thông báo lỗi nếu cần
			ModelState.AddModelError(string.Empty, "Xóa công thức thất bại. Vui lòng thử lại.");
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostUpdateRecipesAsync(int id)
		{
			return RedirectToPage("/Recipes/Update", new {Id = id});
		}
	}
}
