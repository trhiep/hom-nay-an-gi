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
    public class RecipeCommentsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public RecipeCommentsController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/RecipeComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeComment>>> GetRecipeComments()
        {
          if (_context.RecipeComments == null)
          {
              return NotFound();
          }
            return await _context.RecipeComments.ToListAsync();
        }

        // GET: api/RecipeComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeComment>> GetRecipeComment(int id)
        {
          if (_context.RecipeComments == null)
          {
              return NotFound();
          }
            var recipeComment = await _context.RecipeComments.FindAsync(id);

            if (recipeComment == null)
            {
                return NotFound();
            }

            return recipeComment;
        }

        // PUT: api/RecipeComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeComment(int id, RecipeComment recipeComment)
        {
            if (id != recipeComment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(recipeComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCommentExists(id))
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

        // POST: api/RecipeComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeComment>> PostRecipeComment(RecipeComment recipeComment)
        {
          if (_context.RecipeComments == null)
          {
              return Problem("Entity set 'HomNayAnGiContext.RecipeComments'  is null.");
          }
            _context.RecipeComments.Add(recipeComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeComment", new { id = recipeComment.CommentId }, recipeComment);
        }

        // DELETE: api/RecipeComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeComment(int id)
        {
            if (_context.RecipeComments == null)
            {
                return NotFound();
            }
            var recipeComment = await _context.RecipeComments.FindAsync(id);
            if (recipeComment == null)
            {
                return NotFound();
            }

            _context.RecipeComments.Remove(recipeComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeCommentExists(int id)
        {
            return (_context.RecipeComments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
