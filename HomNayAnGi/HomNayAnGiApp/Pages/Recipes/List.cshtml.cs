using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;
using System.Net.Http.Headers;
using HomNayAnGiAPI.Models.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HomNayAnGiApp.Pages.RecipeManage
{
    public class ListModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string RecipeDtoUrl = "http://localhost:5000/api/Recipes/get-list-recipe-dto";
        public ListModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<RecipeDTO> Recipe { get;set; } = default!;

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(RecipeDtoUrl);
            if (response.IsSuccessStatusCode)
            {
                string filmsJSONString = await response.Content.ReadAsStringAsync();
                Recipe = JsonConvert.DeserializeObject<IList<RecipeDTO>>(filmsJSONString);

            }

        }
    }
}
