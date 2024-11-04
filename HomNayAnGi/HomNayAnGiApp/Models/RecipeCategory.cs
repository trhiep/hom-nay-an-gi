using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
{
    public partial class RecipeCategory
    {
        public RecipeCategory()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
