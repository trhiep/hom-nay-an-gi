using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.APIModel;
using HomNayAnGiAPI.Models.DTO.Recipe;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeCategoriesController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public RecipeCategoriesController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/RecipeCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeCategory>>> GetRecipeCategories()
        {
          if (_context.RecipeCategories == null)
          {
              return NotFound();
          }
            return await _context.RecipeCategories.ToListAsync();
        }
        
        [HttpGet("user/{username}")]
        public async Task<ActionResult<IEnumerable<RecipeCategoryDTO>>> GetRecipeCategoriesForAnUser(String username)
        {
            if (_context.RecipeCategories == null)
            {
                return NotFound();
            }

            var recipeCategories = await _context.RecipeCategories.Where(x =>  x.CreatedBy == null).ToListAsync();
            var user = await _context.Users.Where(x => x.Username.Equals(username)).FirstOrDefaultAsync();
            if (user != null)
            {
                recipeCategories.AddRange(_context.RecipeCategories.Where(x => x.CreatedBy == user.UserId));
            }

            var responseCates = new List<RecipeCategoryDTO>();
            foreach (var item in recipeCategories)
            {
                var dto = new RecipeCategoryDTO()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CreatedBy = item.CreatedBy
                };
                responseCates.Add(dto);
            }

            responseCates = responseCates.OrderBy(x => x.CategoryName).ToList();
            
            return Ok(responseCates);
        }

        // GET: api/RecipeCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeCategory>> GetRecipeCategory(int id)
        {
          if (_context.RecipeCategories == null)
          {
              return NotFound();
          }
            var recipeCategory = await _context.RecipeCategories.FindAsync(id);

            if (recipeCategory == null)
            {
                return NotFound();
            }

            return recipeCategory;
        }

        // PUT: api/RecipeCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeCategory(int id, RecipeCategory recipeCategory)
        {
            if (id != recipeCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(recipeCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCategoryExists(id))
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

        // POST: api/RecipeCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeCategory>> PostRecipeCategory(RecipeCategory recipeCategory)
        {
            if (_context.RecipeCategories == null)
            {
                return Problem("Entity set 'HomNayAnGiContext.RecipeCategories'  is null.");
            }
            if (recipeCategory.CreatedBy == 0)
            {
                recipeCategory.CreatedBy = null;
            }
            _context.RecipeCategories.Add(recipeCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeCategory", new { id = recipeCategory.CategoryId }, recipeCategory);
        }

        // DELETE: api/RecipeCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeCategory(int id)
        {
            if (_context.RecipeCategories == null)
            {
                return NotFound();
            }
            var recipeCategory = await _context.RecipeCategories.FindAsync(id);
            if (recipeCategory == null)
            {
                return NotFound();
            }

            _context.RecipeCategories.Remove(recipeCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeCategoryExists(int id)
        {
            return (_context.RecipeCategories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
