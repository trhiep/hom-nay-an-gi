using HomNayAnGiApp.Models.APIModel;
using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string RecipeUrl = "http://localhost:5000/api/Recipes";
        private readonly string RecipeCommentUrl = "http://localhost:5000/api/RecipeComments";

        public DetailsModel()
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            // Retrieve the token from cookies and set it as the Authorization header
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine("SAVED TOKEN = " + accessToken);
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }

        [BindProperty]
        public RecipeDTO RecipeDTO { get; set; }

        [BindProperty]
        public IList<RecipeCommentDTO> RecipeComment { get; set; }

        public async Task<IActionResult> OnGet(int? Id)
        {
            await Console.Out.WriteLineAsync("ID IS:" + Id);
            if (Id != null)
            {
                await Console.Out.WriteLineAsync(_httpClient.DefaultRequestHeaders.ToString());
                HttpResponseMessage response = await _httpClient.GetAsync($"{RecipeUrl}/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseJsonString = await response.Content.ReadAsStringAsync();
                    var recipeDtoApiResponse = JsonConvert.DeserializeObject<ApiResponse<RecipeDTO>>(responseJsonString);
                    if (recipeDtoApiResponse != null)
                    {
                        RecipeDTO = recipeDtoApiResponse.Data;                       
                    }
                }
                //c�i n�y d�nh cho comment
                HttpResponseMessage responseComments = await _httpClient.GetAsync($"{RecipeCommentUrl}/{72}");
                if (responseComments.IsSuccessStatusCode)
                {
                    string responseJsonString = await responseComments.Content.ReadAsStringAsync();
                    var recipeCommentDtoApiResponse = JsonConvert.DeserializeObject<IList<RecipeCommentDTO>>(responseJsonString);
                    if (recipeCommentDtoApiResponse != null)
                    {
                        RecipeComment = recipeCommentDtoApiResponse;
                    }
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
