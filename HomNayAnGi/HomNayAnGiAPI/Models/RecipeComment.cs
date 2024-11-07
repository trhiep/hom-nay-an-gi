using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class RecipeComment
    {
        public RecipeComment()
        {
            InverseParentComment = new HashSet<RecipeComment>();
        }

        public int CommentId { get; set; }
        public int? RecipeId { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ParentCommentId { get; set; }

        public virtual RecipeComment? ParentComment { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<RecipeComment> InverseParentComment { get; set; }
    }
}
