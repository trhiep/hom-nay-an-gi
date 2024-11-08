using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.APIModel;
using HomNayAnGiAPI.Models.DTO;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeCommentsController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;
        private readonly IMapper _mapper;

        public RecipeCommentsController(HomNayAnGiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{recipeId}")]
        public async Task<ActionResult<IEnumerable<RecipeCommentDTO>>> GetAllRecipeCommentsByRecipeId(string recipeId)
        {
            var recipeComments = await _context.RecipeComments
                .Include(rc => rc.User)
                .Where(rc => rc.RecipeId == int.Parse(recipeId))
                .OrderByDescending(rc => rc.CreatedAt)
                .ToListAsync();

            if (recipeComments == null)
            {
                return NotFound();
            }

            // Lấy tất cả các comment cha ban đầu (ParentCommentId == null)
            var parentComments = recipeComments
                .Where(rc => rc.ParentCommentId == null)
                .Select(rc => MapToDto(rc, recipeComments))
                .ToList();

            return Ok(parentComments);
        }

        // Hàm đệ quy để ánh xạ comment và các comment con của nó
        private RecipeCommentDTO MapToDto(RecipeComment comment, List<RecipeComment> allComments)
        {
            return new RecipeCommentDTO
            {
                CommentId = comment.CommentId.ToString(),
                RecipeId = comment.RecipeId.ToString(),
                UserId = comment.UserId.ToString(),
                Username = comment.User.Username,
                Comment = comment.Comment,
                Rating = comment.Rating.ToString(),
                CreatedAt = comment.CreatedAt.ToString(),
                ParentCommentId = comment.ParentCommentId?.ToString(),
        
                // Đệ quy lấy các comment con của comment hiện tại
                Replies = allComments
                    .Where(c => c.ParentCommentId == comment.CommentId)
                    .Select(c => MapToDto(c, allComments)) // Gọi đệ quy để lấy comment con
                    .OrderByDescending(c => c.CreatedAt)
                    .ToList()
            };
        }


        [HttpPost]
        public async Task<ActionResult<RecipeCommentDTO>> PostRecipeComment(RecipeCommentDTO recipeCommentDto)
        {
            if (_context.RecipeComments == null)
            {
                return Problem("Entity set 'HomNayAnGiContext.RecipeComments' is null.");
            }

            // Chuyển đổi `ParentCommentId` một cách an toàn
            int? parentCommentId = null;
            if (!string.IsNullOrEmpty(recipeCommentDto.ParentCommentId) && int.TryParse(recipeCommentDto.ParentCommentId, out int parsedParentId))
            {
                parentCommentId = parsedParentId;
            }

            var recipeComment = new RecipeComment
            {
                RecipeId = int.Parse(recipeCommentDto.RecipeId),
                UserId = int.Parse(recipeCommentDto.UserId),
                Comment = recipeCommentDto.Comment,
                Rating = string.IsNullOrEmpty(recipeCommentDto.Rating) ? 0 : int.Parse(recipeCommentDto.Rating),
                CreatedAt = DateTime.Now,
                ParentCommentId = parentCommentId,
            };

            try
            {
                _context.RecipeComments.Add(recipeComment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(recipeCommentDto);
        }

        // DELETE: api/RecipeComments/delete-comment/{recipeCommentId}/{username}/{recipeId}
        [HttpDelete("delete-comment/{recipeCommentId}/{username}/{recipeId}")]
        public async Task<ApiResponse<int>> DeleteRecipeComment(string recipeCommentId, string username, string recipeId)
        {
            var recipeComment = await _context.RecipeComments
                .Include(x => x.User)
                .Where(rc => rc.CommentId == int.Parse(recipeCommentId)
                && rc.User.Username.Equals(username)
                && rc.RecipeId == int.Parse(recipeId)).FirstOrDefaultAsync();

            if (recipeComment == null || recipeComment.RecipeId != int.Parse(recipeId))
            {
                return new ApiResponse<int>(404, "Không tìm thấy recipe comment");
            }

            _context.RecipeComments.Remove(recipeComment);
            await _context.SaveChangesAsync();

            return new ApiResponse<int>(203);
        }

        // PUT: api/RecipeComments/update-comment/{recipeCommentId}/{username}/{recipeId}
        [HttpPut("update-comment/{recipeCommentId}/{username}/{recipeId}")]
        public async Task<IActionResult> UpdateRecipeComment(string recipeCommentId, string username, string recipeId, RecipeCommentDTO recipeCommentDto)
        {
            // Chuyển đổi an toàn các giá trị ID
            if (!int.TryParse(recipeCommentId, out int commentId) || !int.TryParse(recipeId, out int parsedRecipeId))
            {
                return BadRequest("Invalid Comment ID or Recipe ID format.");
            }

            var recipeComment = await _context.RecipeComments
                .Include(x => x.User)
                .Where(rc => rc.CommentId == commentId
                             && rc.User != null
                             && rc.User.Username.Equals(username)
                             && rc.RecipeId == parsedRecipeId)
                .FirstOrDefaultAsync();

            if (recipeComment == null)
            {
                return NotFound("Comment not found or username mismatch.");
            }

            // Cập nhật nội dung bình luận
            recipeComment.Comment = recipeCommentDto.Comment;

            // Đánh dấu thực thể là đã thay đổi
            _context.Entry(recipeComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCommentExists(commentId))
                {
                    return NotFound("Comment no longer exists.");
                }
                else
                {
                    throw; // Ném ngoại lệ nếu không phải do comment không tồn tại
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
