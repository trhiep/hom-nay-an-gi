using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.DTO;

namespace HomNayAnGiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly HomNayAnGiContext _context;

        public RecipesController(HomNayAnGiContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet("get-list-recipe-dto")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipesDTO()
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            return await _context.Recipes.Include(x => x.Category).Select(

             item => new RecipeDTO
             {
                 RecipeId = item.RecipeId,
                 CategoryName = item.Category.CategoryName,
                 Description = item.Description,
                 CookTime = item.CookTime,
                 PrepTime = item.PrepTime,
                 Servings = item.Servings,
                 DifficultyLevel = item.DifficultyLevel,
                 UserId = item.UserId,
                 CreatedAt = item.CreatedAt,
                 UpdatedAt = item.UpdatedAt,
                 Image = item.Image,
                 Video = item.Video,
                 IsPublic = item.IsPublic
             }
         ).ToListAsync();
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            return await _context.Recipes.ToListAsync();
        }
        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ApiResponse<int>> PostRecipe(RecipeCreateRequest recipeCreateRequestModel)
        {
            var user = await _context.Users.Where(x => x.Username.Equals(recipeCreateRequestModel.Username))
                .FirstOrDefaultAsync();
            Recipe newRecipe = new Recipe()
            {
                RecipeName = recipeCreateRequestModel.RecipeName,
                Description = recipeCreateRequestModel.Description == "" ? null : recipeCreateRequestModel.Description,
                PrepTime = recipeCreateRequestModel.PrepTime == 0 ? null : recipeCreateRequestModel.PrepTime,
                CookTime = recipeCreateRequestModel.CookTime == 0 ? null : recipeCreateRequestModel.CookTime,
                DifficultyLevel = recipeCreateRequestModel.DifficultyLevel == "" ? null : recipeCreateRequestModel.DifficultyLevel,
                UserId = user?.UserId,
                CategoryId = recipeCreateRequestModel.CategoryId == 0 ? null : recipeCreateRequestModel.CategoryId,
                Image = recipeCreateRequestModel.Image == "" ? null : recipeCreateRequestModel.Image
            };

            _context.Recipes.Add(newRecipe);
            int result = await _context.SaveChangesAsync();
            return new ApiResponse<int>(result, "");
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
