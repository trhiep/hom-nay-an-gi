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

        // GET: api/UserFavorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFavorite>>> GetUserFavorites()
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }
            return await _context.UserFavorites.ToListAsync();
        }

        // GET: api/UserFavorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFavorite>> GetUserFavorite(int id)
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }
            var userFavorite = await _context.UserFavorites.FindAsync(id);

            if (userFavorite == null)
            {
                return NotFound();
            }

            return userFavorite;
        }

        // PUT: api/UserFavorites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFavorite(int id, UserFavorite userFavorite)
        {
            if (id != userFavorite.UserFavoriteId)
            {
                return BadRequest();
            }

            _context.Entry(userFavorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFavoriteExists(id))
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

        // POST: api/UserFavorites
        //hàm này nhận userId và RecipeId xong parse sang int rồi lưu vào bảng UserFavorite
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
        //hàm này nhận về string userFavoriteId rồi xóa
        [HttpDelete("{userFavoriteId}")]
        public async Task<IActionResult> DeleteUserFavorite(string userFavoriteId)
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }
            var userFavorite = await _context.UserFavorites.FindAsync(int.Parse(userFavoriteId));
            if (userFavorite == null)
            {
                return NotFound();
            }

            _context.UserFavorites.Remove(userFavorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserFavoriteExists(int id)
        {
            return (_context.UserFavorites?.Any(e => e.UserFavoriteId == id)).GetValueOrDefault();
        }

        //hàm này check xem user đấy đã add favourite recipe chưa
        [HttpGet("{userId}/{recipeId}")]
        public async Task<ActionResult<UserFavorite>> GetUserFavouriteByUserIdRecipeId(string userId, string recipeId)
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }
            var userFavorite = await _context.UserFavorites
                .Where(uf => uf.UserId == int.Parse(userId) && uf.RecipeId == int.Parse(recipeId))
                .FirstOrDefaultAsync();
            if (userFavorite == null)
            {
                return null;
            }

            return userFavorite;
        }

        //hàm này lấy ra list UserFavorite theo userId
        [HttpGet("get-all-recipe-favorite/{userId}")]
        public async Task<ActionResult<IEnumerable<UserFavorite>>> GetAllMyFavoriteRecipe(string userId)
        {
            if (_context.UserFavorites == null)
            {
                return NotFound();
            }

            return await _context.UserFavorites.Where(uf => uf.UserId == int.Parse(userId)).ToListAsync();
        }
    }
}
