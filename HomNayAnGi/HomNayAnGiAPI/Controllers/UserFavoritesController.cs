using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Humanizer.Localisation.TimeToClockNotation;
using HomNayAnGiAPI.Models.DTO;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoritesController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public UserFavoritesController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/UserFavorites/get-all-my-recipes/{username}
        [HttpGet("get-all-my-recipes/{username}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetUserFavorites(string username)
        {
            int? userId = await GetUserIdByName(username);

            if (userId == null)
            {
                return NotFound("User not found.");
            }

            var userFavorites = await _context.UserFavorites
                .Where(uf => uf.UserId == userId.Value)
                .Include(uf => uf.Recipe) // Include related Recipe data
                .Select(uf => uf.Recipe)  // Select only Recipe information
                .ToListAsync();

            if (!userFavorites.Any())
            {
                return NotFound("No favorite recipes found for this user.");
            }

            return Ok(userFavorites);
        }

        private async Task<int?> GetUserIdByName(string username)
        {
            // Get the UserId from the Users table based on the username
            return await _context.Users
                .Where(u => u.Username.Equals(username))
                .Select(u => u.UserId)
                .SingleOrDefaultAsync();
        }

        // POST: api/UserFavorites
        //hàm này nhận username và recipeid xong parse sang int rồi lưu vào bảng UserFavorite
        [HttpPost]
        public async Task<ActionResult<UserFavorite>> PostUserFavorite(UserFavoriteDTO userFavorite)
        {
            if (_context.UserFavorites == null)
            {
                return Problem("Entity set 'HomNayAnGiContext.UserFavorites'  is null.");
            }
            UserFavorite uf = new UserFavorite();
            uf.UserId = int.Parse(userFavorite.UserId);
            uf.RecipeId = int.Parse(userFavorite.RecipeId);
            _context.UserFavorites.Add(uf);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/UserFavorites/5
        //hàm này nhận về string username và recipeid rồi xóa
        [HttpDelete("delete/{username}/{recipeId}")]
        public async Task<IActionResult> DeleteUserFavorite(string username, string recipeId)
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }

            int? userId = await GetUserIdByName(username);
            // Check if the userId exists
            if (userId == 0)
            {
                return NotFound("User not found");
            }

            // Find the UserFavorite record based on userId and recipeId
            var userFavorite = await _context.UserFavorites
                .Where(uf => uf.UserId == userId && uf.RecipeId == int.Parse(recipeId))
                .FirstOrDefaultAsync();

            if (userFavorite == null)
            {
                return NotFound("User favorite not found");
            }

            _context.UserFavorites.Remove(userFavorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //hàm này check xem user đấy đã add favourite recipe chưa
        [HttpGet("{username}/{recipeId}")]
        public async Task<ActionResult<UserFavorite>> GetUserFavouriteByUserIdRecipeId(string username, string recipeId)
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }
            int? userId = await GetUserIdByName(username);
            var userFavorite = await _context.UserFavorites
                .Where(uf => uf.UserId == userId && uf.RecipeId == int.Parse(recipeId))
                .FirstOrDefaultAsync();
            if (userFavorite == null)
            {
                return null;
            }

            return userFavorite;
        }
    }
}
