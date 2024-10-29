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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserFavorite>> PostUserFavorite(UserFavorite userFavorite)
        {
          if (_context.UserFavorites == null)
          {
              return Problem("Entity set 'HomNayAnGiContext.UserFavorites'  is null.");
          }
            _context.UserFavorites.Add(userFavorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFavorite", new { id = userFavorite.UserFavoriteId }, userFavorite);
        }

        // DELETE: api/UserFavorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFavorite(int id)
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
    }
}
