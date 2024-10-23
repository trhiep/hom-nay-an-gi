using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomNayAnGiAPI.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeComments = new HashSet<RecipeComment>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
            RecipeSteps = new HashSet<RecipeStep>();
            Users = new HashSet<User>();
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
        public int IsPublic { get; set; }
        public string? Video { get; set; }

        public virtual RecipeCategory? Category { get; set; }
        public virtual NutritionFact? NutritionFact { get; set; }

        [JsonIgnore]
        public virtual ICollection<RecipeComment> RecipeComments { get; set; }
        [JsonIgnore]
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        [JsonIgnore]
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
