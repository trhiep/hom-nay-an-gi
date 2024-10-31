namespace HomNayAnGiAPI.Models.DTO
{
    public class RecipeCommentDTO
    {
        public int CommentId { get; set; }
        public int? RecipeId { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public string? CreatedAt { get; set; } // Store as string for easier parsing
    }
}
