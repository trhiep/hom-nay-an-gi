using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace HomNayAnGiApp.Pages.RecipeIngredients
{
    public class DeleteModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;
        private readonly string IngredientUrl = "http://localhost:5000/api/Ingredients/";
        private readonly HttpClient _httpClient;
        public DeleteModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _context = context;
        }

        [BindProperty]
      public Ingredient Ingredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            
            HttpResponseMessage response = await _httpClient.GetAsync(IngredientUrl + id);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Ingredient = JsonConvert.DeserializeObject<Ingredient>(jsonStr);
                return Page();
            }

            if (Ingredient == null)
            {
                return NotFound();
            }
          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await _httpClient.DeleteAsync(IngredientUrl+id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
