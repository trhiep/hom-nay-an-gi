using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomNayAnGiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HomNayAnGiApp.Pages.RecipeIngredientManage
{
    public class CreateModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;

        public CreateModel(HomNayAnGiApp.Models.HomNayAnGiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "IngredientId", "IngredientName");
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeName");
            return Page();
        }

        [BindProperty]
        public RecipeIngredient RecipeIngredient { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RecipeIngredients == null || RecipeIngredient == null)
            {
                return Page();
            }

            _context.RecipeIngredients.Add(RecipeIngredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
