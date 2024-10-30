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
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public RecipeIngredientsController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/RecipeIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredient>>> GetRecipeIngredients()
        {
          if (_context.RecipeIngredients == null)
          {
              return NotFound();
          }
            return await _context.RecipeIngredients.ToListAsync();
        }

        // GET: api/RecipeIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeIngredient>> GetRecipeIngredient(int id)
        {
          if (_context.RecipeIngredients == null)
          {
              return NotFound();
          }
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);

            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return recipeIngredient;
        }
        
        // GET: api/RecipeIngredients/5
        [HttpGet("recipe/{id}")]
        public async Task<ActionResult<List<RecipeIngredientDTO>>> GetRecipeIngredientByRecipeId(int id)
        {
            if (_context.RecipeIngredients == null)
            {
                return NotFound();
            }
    
            var recipeIngredients = await _context.RecipeIngredients
                .Where(x => x.RecipeId == id).Include(x => x.Ingredient)
                .Select(item => new RecipeIngredientDTO
                {
                    RecipeIngredientId = item.IngredientId,
                    RecipeId = item.RecipeId,
                    IngredientId = item.IngredientId,
                    IngredientName = item.Ingredient.IngredientName,
                    Quantity = item.Quantity,
                    Unit = item.Unit
                })
                .ToListAsync();
    
            return Ok(recipeIngredients);
        }


        // PUT: api/RecipeIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeIngredient(int id, RecipeIngredient recipeIngredient)
        {
            if (id != recipeIngredient.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipeIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(id))
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

        // POST: api/RecipeIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> PostRecipeIngredient(RecipeIngredientCreateModel recipeIngredientCreateModel)
        {
            Console.WriteLine("ĐÃ VÀO POST");
            Console.WriteLine(recipeIngredientCreateModel.ToString());
            foreach (var recipeIngredient in recipeIngredientCreateModel.RecipeIngredients)
            {
                RecipeIngredient newRecipeIngredient = new RecipeIngredient()
                {
                    RecipeId = recipeIngredientCreateModel.RecipeId,
                    IngredientId = recipeIngredient.IngredientId,
                    Quantity = recipeIngredient.Quantity,
                    Unit = recipeIngredient.Unit
                };
                _context.RecipeIngredients.Add(newRecipeIngredient);
            }

            await _context.SaveChangesAsync();
            return Ok(new ApiResponse<int>(201));
        }

        // DELETE: api/RecipeIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(int id)
        {
            if (_context.RecipeIngredients == null)
            {
                return NotFound();
            }
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeIngredientExists(int id)
        {
            return (_context.RecipeIngredients?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
