using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
