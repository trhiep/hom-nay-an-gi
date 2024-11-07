﻿using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
{
    public partial class User
    {
        public User()
        {
            Ingredients = new HashSet<Ingredient>();
            RecipeCategories = new HashSet<RecipeCategory>();
            RecipeComments = new HashSet<RecipeComment>();
            Recipes = new HashSet<Recipe>();
            UserFavorites = new HashSet<UserFavorite>();
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }
        public virtual ICollection<RecipeComment> RecipeComments { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
