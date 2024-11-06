using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
{
    public partial class NutritionFact
    {
        public int RecipeId { get; set; }
        public double? Calories { get; set; }
        public double? Protein { get; set; }
        public double? Fat { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Fiber { get; set; }
        public double? Sugar { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
