using System;
using System.Collections.Generic;

namespace HomNayAnGiApp.Models
{
    public partial class RecipeComment
    {
        public int CommentId { get; set; }
        public int? RecipeId { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Recipe? Recipe { get; set; }
        public virtual User? User { get; set; }
    }
}
