using HomNayAnGiApp.Models.APIModel;
using HomNayAnGiApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HomNayAnGiApp.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string RecipeUrl = "http://localhost:5000/api/Recipes";
        private readonly string RecipeCommentUrl = "http://localhost:5000/api/RecipeComments";

        public DetailsModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
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
                //cái này dành cho comment
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
