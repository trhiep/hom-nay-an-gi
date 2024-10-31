namespace HomNayAnGiAPI.Models.DTO
{
    public class RecipeCommentDTO
    {
        public string? CommentId { get; set; }
        public string? RecipeId { get; set; }
        public string? UserId { get; set; }
        public string? Comment { get; set; }
        public string? Rating { get; set; }
        public string? CreatedAt { get; set; } // Store as string for easier parsing
    }
}
