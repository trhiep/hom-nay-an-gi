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
    public class DeleteModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;

        public DeleteModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RecipeIngredients == null)
            {
                return NotFound();
            }
            var recipeingredient = await _context.RecipeIngredients.FindAsync(id);

            if (recipeingredient != null)
            {
                RecipeIngredient = recipeingredient;
                _context.RecipeIngredients.Remove(RecipeIngredient);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
