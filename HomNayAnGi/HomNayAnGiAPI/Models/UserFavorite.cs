using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class UserFavorite
    {
        public int UserFavoriteId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
