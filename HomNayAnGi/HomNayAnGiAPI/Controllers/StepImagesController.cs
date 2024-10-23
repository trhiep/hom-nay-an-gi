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
    public class StepImagesController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public StepImagesController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/StepImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StepImage>>> GetStepImages()
        {
          if (_context.StepImages == null)
          {
              return NotFound();
          }
            return await _context.StepImages.ToListAsync();
        }

        // GET: api/StepImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StepImage>> GetStepImage(int id)
        {
          if (_context.StepImages == null)
          {
              return NotFound();
          }
            var stepImage = await _context.StepImages.FindAsync(id);

            if (stepImage == null)
            {
                return NotFound();
            }

            return stepImage;
        }

        // PUT: api/StepImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStepImage(int id, StepImage stepImage)
        {
            if (id != stepImage.StepImageId)
            {
                return BadRequest();
            }

            _context.Entry(stepImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepImageExists(id))
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

        // POST: api/StepImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StepImage>> PostStepImage(StepImage stepImage)
        {
          if (_context.StepImages == null)
          {
              return Problem("Entity set 'HomNayAnGiContext.StepImages'  is null.");
          }
            _context.StepImages.Add(stepImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStepImage", new { id = stepImage.StepImageId }, stepImage);
        }

        // DELETE: api/StepImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStepImage(int id)
        {
            if (_context.StepImages == null)
            {
                return NotFound();
            }
            var stepImage = await _context.StepImages.FindAsync(id);
            if (stepImage == null)
            {
                return NotFound();
            }

            _context.StepImages.Remove(stepImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StepImageExists(int id)
        {
            return (_context.StepImages?.Any(e => e.StepImageId == id)).GetValueOrDefault();
        }
    }
}
