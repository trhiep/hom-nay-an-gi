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
    public class DetailsModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;

        public DetailsModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _context = context;
        }

      public RecipeIngredient RecipeIngredient { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RecipeIngredients == null)
            {
                return NotFound();
            }

            var recipeingredient = await _context.RecipeIngredients.FirstOrDefaultAsync(m => m.RecipeIngredientId == id);
            if (recipeingredient == null)
            {
                return NotFound();
            }
            else 
            {
                RecipeIngredient = recipeingredient;
            }
            return Page();
        }
    }
}
