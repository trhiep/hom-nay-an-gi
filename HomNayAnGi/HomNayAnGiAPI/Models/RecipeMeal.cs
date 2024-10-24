using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class RecipeMeal
    {
        public int RecipeMealId { get; set; }
        public int MealId { get; set; }
        public int RecipeId { get; set; }

        public virtual Meal Meal { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
