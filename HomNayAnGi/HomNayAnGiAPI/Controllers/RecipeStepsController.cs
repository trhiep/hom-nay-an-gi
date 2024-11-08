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
using AutoMapper;
using SQLitePCL;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeStepsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;
        private readonly IMapper _mapper;

        public RecipeStepsController(HomNayAnGiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                .OrderBy(x => x.StepNumber)
                .Include(x => x.StepImages)
                .ToListAsync();

            var recipeStepDto = _mapper.Map<List<RecipeStepDTO>>(recipeSteps);

            return Ok(recipeStepDto);
        }

        // PUT: api/RecipeSteps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeStep(int id, RecipeStepDTO recipeStep)
        {
            if (id != recipeStep.StepId)
            {
                return BadRequest();
            }

            var editRecipeStep = await _context.RecipeSteps.Where(x => x.StepId == recipeStep.StepId).FirstOrDefaultAsync();
            if (editRecipeStep == null)
            {
                return NotFound();
            }

            editRecipeStep.StepNumber = recipeStep.StepNumber;
            editRecipeStep.Instruction = recipeStep.Instruction;
            _context.RecipeSteps.Update(editRecipeStep);
            await _context.SaveChangesAsync();

            foreach (var img in recipeStep.StepImages)
            {
                var thisImg = await _context.StepImages.Where(x => x.StepImageId == img.StepImageId).FirstOrDefaultAsync();
                if (thisImg != null)
                {
                    thisImg.ImageLink = img.ImageLink;
                    _context.StepImages.Update(thisImg);
                    await _context.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        // POST: api/RecipeSteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> PostRecipeStep(List<RecipeStepDTO> recipeSteps)
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
                await _context.SaveChangesAsync();

                foreach (var stepImg in step.StepImages)
                {
                    StepImage stepImage = new StepImage()
                    {
                        ImageLink = stepImg.ImageLink,
                        StepId = recipeStep.StepId
                    };
                    _context.StepImages.Add(stepImage);
                    await _context.SaveChangesAsync();
                }
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

            int recipeId = (int)recipeStep.RecipeId;

            var stepImage = await _context.StepImages.Where(x => x.StepId == id).ToListAsync();
            _context.StepImages.RemoveRange(stepImage);
            await _context.SaveChangesAsync();

            _context.RecipeSteps.Remove(recipeStep);
            await _context.SaveChangesAsync();

            var recipeSteps = await _context.RecipeSteps
                .Where(x => x.RecipeId == recipeId)
                .OrderBy(x => x.StepNumber)
                .ToListAsync();
            int count = 1;
            foreach (var step in recipeSteps)
            {
                step.StepNumber = count;
                count++;
            }

            _context.RecipeSteps.UpdateRange(recipeSteps);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RecipeStepExists(int id)
        {
            return (_context.RecipeSteps?.Any(e => e.StepId == id)).GetValueOrDefault();
        }
    }
}