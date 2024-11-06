using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
{
    public partial class Meal
    {
        public Meal()
        {
            RecipeMeals = new HashSet<RecipeMeal>();
        }

        public int MealId { get; set; }
        public string MealName { get; set; } = null!;

        public virtual ICollection<RecipeMeal> RecipeMeals { get; set; }
    }
}
