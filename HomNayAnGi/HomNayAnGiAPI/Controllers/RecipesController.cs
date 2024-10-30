using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.DTO;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public RecipesController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipes()
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            return await _context.Recipes.Include(x => x.Category).Select(

     item => new Models.DTO.RecipeDTO
     {
         RecipeId = item.RecipeId.ToString(),
         CategoryName = item.Category.CategoryName.ToString(),
         Description = item.Description.ToString(),
         CookTime = item.CookTime.ToString(),
         PrepTime = item.PrepTime.ToString(),
         Servings = item.Servings.ToString(),
         DifficultyLevel = item.DifficultyLevel.ToString(),
         UserId = item.UserId.ToString(),
         CreatedAt = item.CreatedAt.ToString(),
         UpdatedAt = item.UpdatedAt.ToString(),
         Image = item.Image.ToString(),
         Video = item.Video.ToString(),
         IsPublic = item.IsPublic.ToString()
     }
     ).ToListAsync();
        }

        //// GET: api/Recipes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        //{
        //    if (_context.Recipes == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Recipes.ToListAsync();
        //}
        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'HomNayAnGiContext.Recipes'  is null.");
            }
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
