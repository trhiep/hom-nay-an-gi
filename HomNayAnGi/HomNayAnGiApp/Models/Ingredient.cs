using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
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
        public int? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
