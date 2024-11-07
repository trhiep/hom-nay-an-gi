using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomNayAnGiApp.Models;

namespace HomNayAnGiApp.Pages.RecipeIngredients
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
        ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Ingredients == null || Ingredient == null)
            {
                return Page();
            }



            _context.Ingredients.Add(Ingredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
