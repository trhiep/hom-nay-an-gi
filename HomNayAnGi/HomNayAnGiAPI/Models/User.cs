using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomNayAnGiAPI.Models
{
    public partial class User
    {
        public User()
        {
            RecipeComments = new HashSet<RecipeComment>();
            Recipes = new HashSet<Recipe>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual ICollection<RecipeComment> RecipeComments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
