using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;

namespace HomNayAnGiApp.Pages.RecipeIngredients
{
    public class IndexModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;
        private readonly string IngredientFilmUrl = "http://localhost:5333/api/Ingredients";
        private readonly HttpClient _httpClient;
        public IndexModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Ingredients != null)
            {
                Ingredient = await _context.Ingredients
                .Include(i => i.CreatedByNavigation).ToListAsync();
            }
        }
    }
}
