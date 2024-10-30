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
    public class RecipeStepsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public RecipeStepsController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/RecipeSteps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeStep>>> GetRecipeSteps()
        {
            if (_context.RecipeSteps == null)
            {
                return NotFound();
            }

            return await _context.RecipeSteps.ToListAsync();
        }

        // GET: api/RecipeSteps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeStep>> GetRecipeStep(int id)
        {
            if (_context.RecipeSteps == null)
            {
                return NotFound();
            }

            var recipeStep = await _context.RecipeSteps.FindAsync(id);

            if (recipeStep == null)
            {
                return NotFound();
            }

            return recipeStep;
        }
        
        // GET: api/RecipeSteps/5
        [HttpGet("recipe/{id}")]
        public async Task<ActionResult<List<RecipeStepDTO>>> GetRecipeStepsByRecipeId(int id)
        {
            if (_context.RecipeSteps == null)
            {
                return NotFound();
            }

            var recipeSteps = await _context.RecipeSteps
                .Where(x => x.RecipeId == id)
                .Select(item => new RecipeStepDTO()
                {
                    StepId = item.StepId,
                    Instruction = item.Instruction,
                    RecipeId = item.RecipeId,
                    StepNumber = item.StepNumber
                })
                .OrderBy(x => x.StepNumber)
                .ToListAsync();
    
            return Ok(recipeSteps);
        }

        // PUT: api/RecipeSteps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeStep(int id, RecipeStep recipeStep)
        {
            if (id != recipeStep.StepId)
            {
                return BadRequest();
            }

            _context.Entry(recipeStep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeStepExists(id))
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

        // POST: api/RecipeSteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> PostRecipeStep(List<RecipeStep> recipeSteps)
        {
            if (_context.RecipeSteps == null)
            {
                return Problem("Entity set 'HomNayAnGiContext.RecipeSteps'  is null.");
            }

            foreach (var step in recipeSteps)
            {
                RecipeStep recipeStep = new RecipeStep()
                {
                    RecipeId = step.RecipeId,
                    StepNumber = step.StepNumber,
                    Instruction = step.Instruction
                };
                _context.RecipeSteps.Add(recipeStep);
            }

            int results = await _context.SaveChangesAsync();
            return Ok(new ApiResponse<int>(results));
        }

        // DELETE: api/RecipeSteps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeStep(int id)
        {
            if (_context.RecipeSteps == null)
            {
                return NotFound();
            }

            var recipeStep = await _context.RecipeSteps.FindAsync(id);
            if (recipeStep == null)
            {
                return NotFound();
            }

            _context.RecipeSteps.Remove(recipeStep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeStepExists(int id)
        {
            return (_context.RecipeSteps?.Any(e => e.StepId == id)).GetValueOrDefault();
        }
    }
}