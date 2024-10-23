using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class RecipeCategory
    {
        public RecipeCategory()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
