using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomNayAnGiAPI.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public string? Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
