using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.DTO;

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

        // GET: api/RecipeComments/{recipeId}
        [HttpGet("{recipeId}")]
        public async Task<ActionResult<IEnumerable<RecipeCommentDTO>>> GetAllRecipeCommentsByRecipeId(string recipeId)
        {
            var recipeComments = await _context.RecipeComments
                .Include(rc => rc.User)
                .Where(rc => rc.RecipeId == int.Parse(recipeId))
                .Select(rc => new RecipeCommentDTO
                {
                    CommentId = rc.CommentId.ToString(),
                    RecipeId = rc.RecipeId.ToString(),
                    UserId = rc.UserId.ToString(),
                    Username = rc.User.Username,
                    Comment = rc.Comment,
                    Rating = rc.Rating.ToString(),
                    CreatedAt = rc.CreatedAt.ToString(),
                    ParentCommentId = rc.ParentCommentId.ToString()
                })
                .OrderByDescending(rc => rc.CreatedAt) // Newest comments first
                .ToListAsync();

            return Ok(recipeComments);
        }

        // POST: api/RecipeComments
        [HttpPost]
        public async Task<ActionResult<RecipeCommentDTO>> PostRecipeComment(RecipeCommentDTO recipeCommentDto)
        {
            if (_context.RecipeComments == null)
            {
                return Problem("Entity set 'HomNayAnGiContext.RecipeComments' is null.");
            }

            var recipeComment = new RecipeComment
            {
                RecipeId = int.Parse(recipeCommentDto.RecipeId),
                UserId = int.Parse(recipeCommentDto.UserId),
                Comment = recipeCommentDto.Comment,
                Rating = recipeCommentDto.Rating == null ? 0 : int.Parse(recipeCommentDto.Rating),
                CreatedAt = DateTime.Now
            };

            _context.RecipeComments.Add(recipeComment);
            await _context.SaveChangesAsync();

            recipeCommentDto.CommentId = recipeComment.CommentId.ToString();
            return Ok(recipeCommentDto);
        }

        // DELETE: api/RecipeComments/delete-comment/{recipeCommentId}/{username}/{recipeId}
        [HttpDelete("delete-comment/{recipeCommentId}/{username}/{recipeId}")]
        public async Task<IActionResult> DeleteRecipeComment(string recipeCommentId, string username, string recipeId)
        {
            if (_context.RecipeComments == null)
            {
                return NotFound();
            }
            var recipeComment = await _context.RecipeComments
                .Include(x => x.User)
                .Where(rc => rc.CommentId == int.Parse(recipeCommentId)
                && rc.User.Username.Equals(username)
                && rc.RecipeId == int.Parse(recipeId)).FirstOrDefaultAsync();

            if (recipeComment == null || recipeComment.RecipeId != int.Parse(recipeId))
            {
                return NotFound();
            }

            _context.RecipeComments.Remove(recipeComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/RecipeComments/update-comment/{recipeCommentId}/{username}/{recipeId}
        [HttpPut("update-comment/{recipeCommentId}/{username}/{recipeId}")]
        public async Task<IActionResult> UpdateRecipeComment(string recipeCommentId, string username, string recipeId, RecipeCommentDTO recipeCommentDto)
        {
            if (recipeCommentId != recipeCommentDto.CommentId || recipeId != recipeCommentDto.RecipeId)
            {
                return BadRequest();
            }

            var recipeComment = await _context.RecipeComments
                .Include(x => x.User)
                .Where(rc => rc.CommentId == int.Parse(recipeCommentId)
                && rc.User.Username.Equals(username)
                && rc.RecipeId == int.Parse(recipeId)).FirstOrDefaultAsync();

            if (recipeComment == null || recipeComment.RecipeId != int.Parse(recipeId))
            {
                return NotFound();
            }

            recipeComment.Comment = recipeCommentDto.Comment;
            recipeComment.Rating = Int32.Parse(recipeCommentDto.Rating);
            recipeComment.CreatedAt = DateTime.Parse(recipeCommentDto.CreatedAt);

            _context.Entry(recipeComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCommentExists(int.Parse(recipeCommentId)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(recipeCommentDto);
        }

        private bool RecipeCommentExists(int id)
        {
            return (_context.RecipeComments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
