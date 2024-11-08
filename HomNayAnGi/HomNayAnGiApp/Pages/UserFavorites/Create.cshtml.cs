using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomNayAnGiApp.Models;

namespace HomNayAnGiApp.Pages.UserFavorites
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
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public UserFavorite UserFavorite { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserFavorites == null || UserFavorite == null)
            {
                return Page();
            }

            _context.UserFavorites.Add(UserFavorite);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
