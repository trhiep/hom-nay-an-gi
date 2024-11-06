using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeComments = new HashSet<RecipeComment>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
            RecipeMeals = new HashSet<RecipeMeal>();
            RecipeSteps = new HashSet<RecipeStep>();
            UserFavorites = new HashSet<UserFavorite>();
        }

        public int RecipeId { get; set; }
        public int? CategoryId { get; set; }
        public string RecipeName { get; set; } = null!;
        public string? Description { get; set; }
        public int? CookTime { get; set; }
        public int? PrepTime { get; set; }
        public int? Servings { get; set; }
        public string? DifficultyLevel { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public int IsPublic { get; set; }

        public virtual RecipeCategory? Category { get; set; }
        public virtual NutritionFact? NutritionFact { get; set; }
        public virtual ICollection<RecipeComment> RecipeComments { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<RecipeMeal> RecipeMeals { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
    }
}
