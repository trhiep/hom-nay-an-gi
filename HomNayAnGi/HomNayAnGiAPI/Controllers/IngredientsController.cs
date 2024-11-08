using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.DTO;
using HomNayAnGiAPI.Models.DTO.Recipe;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public IngredientsController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredients()
        {
          if (_context.Ingredients == null)
          {
              return NotFound();
          }
          var ingredients = await _context.Ingredients.ToListAsync();
            List<IngredientDTO> ingredientDTOs = new List<IngredientDTO>();
            foreach (var item in ingredients)
            {
                ingredientDTOs.Add(new IngredientDTO()
                {
                    IngredientId = item.IngredientId,
                    IngredientName = item.IngredientName,
                    Description = item.Description,
                    CreatedBy = item.CreatedBy
                });
            }
            return ingredientDTOs;
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
          if (_context.Ingredients == null)
          {
              return NotFound();
          }
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }
        
        [HttpGet("user/{username}")]
        public async Task<ActionResult<Ingredient>> GetIngredientsForAnUser(String username)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            
            var ingredients = await _context.Ingredients.Where(x =>  x.CreatedBy == null).ToListAsync();
            var user = await _context.Users.Where(x => x.Username.Equals(username)).FirstOrDefaultAsync();
            if (user != null)
            {
                ingredients.AddRange(_context.Ingredients.Where(x => x.CreatedBy == user.UserId));
            }
            
            var responseIngredients = new List<IngredientDTO>();
            foreach (var item in ingredients)
            {
                var dto = new IngredientDTO()
                {
                    IngredientId = item.IngredientId,
                    IngredientName = item.IngredientName,
                    Description = item.Description,
                    CreatedBy = item.CreatedBy
                };
                responseIngredients.Add(dto);
            }

            responseIngredients = responseIngredients.OrderBy(x => x.IngredientName).ToList();
            
            return Ok(responseIngredients);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
          if (_context.Ingredients == null)
          {
              return Problem("Entity set 'HomNayAnGiContext.Ingredients'  is null.");
          }

          Console.WriteLine("SUBMITED: " + ingredient.CreatedBy);

            ingredient.CreatedBy = ingredient.CreatedBy == 0 ? null : ingredient.CreatedBy;
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return (_context.Ingredients?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }
    }
}
