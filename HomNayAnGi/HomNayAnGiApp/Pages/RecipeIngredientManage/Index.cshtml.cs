using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;

namespace HomNayAnGiApp.Pages.RecipeIngredientManage
{
    public class IndexModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;

        public IndexModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _context = context;
        }

        public IList<RecipeIngredient> RecipeIngredient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RecipeIngredients != null)
            {
                RecipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe).ToListAsync();
            }
        }
    }
}
