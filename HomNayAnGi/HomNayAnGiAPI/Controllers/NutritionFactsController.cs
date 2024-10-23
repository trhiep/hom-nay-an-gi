using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionFactsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public NutritionFactsController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/NutritionFacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutritionFact>>> GetNutritionFacts()
        {
          if (_context.NutritionFacts == null)
          {
              return NotFound();
          }
            return await _context.NutritionFacts.ToListAsync();
        }

        // GET: api/NutritionFacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NutritionFact>> GetNutritionFact(int id)
        {
          if (_context.NutritionFacts == null)
          {
              return NotFound();
          }
            var nutritionFact = await _context.NutritionFacts.FindAsync(id);

            if (nutritionFact == null)
            {
                return NotFound();
            }

            return nutritionFact;
        }

        // PUT: api/NutritionFacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutritionFact(int id, NutritionFact nutritionFact)
        {
            if (id != nutritionFact.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(nutritionFact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionFactExists(id))
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

        // POST: api/NutritionFacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NutritionFact>> PostNutritionFact(NutritionFact nutritionFact)
        {
          if (_context.NutritionFacts == null)
          {
              return Problem("Entity set 'HomNayAnGiContext.NutritionFacts'  is null.");
          }
            _context.NutritionFacts.Add(nutritionFact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NutritionFactExists(nutritionFact.RecipeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNutritionFact", new { id = nutritionFact.RecipeId }, nutritionFact);
        }

        // DELETE: api/NutritionFacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionFact(int id)
        {
            if (_context.NutritionFacts == null)
            {
                return NotFound();
            }
            var nutritionFact = await _context.NutritionFacts.FindAsync(id);
            if (nutritionFact == null)
            {
                return NotFound();
            }

            _context.NutritionFacts.Remove(nutritionFact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NutritionFactExists(int id)
        {
            return (_context.NutritionFacts?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
