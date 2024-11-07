using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;

namespace HomNayAnGiApp.Pages.RecipeIngredientManage
{
    public class EditModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;

        public EditModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
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

            var recipeingredient =  await _context.RecipeIngredients.FirstOrDefaultAsync(m => m.RecipeIngredientId == id);
            if (recipeingredient == null)
            {
                return NotFound();
            }
            RecipeIngredient = recipeingredient;
           ViewData["IngredientId"] = new SelectList(_context.Ingredients, "IngredientId", "IngredientId");
           ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RecipeIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(RecipeIngredient.RecipeIngredientId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeIngredientExists(int id)
        {
          return (_context.RecipeIngredients?.Any(e => e.RecipeIngredientId == id)).GetValueOrDefault();
        }
    }
}
