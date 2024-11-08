namespace HomNayAnGiApp.Models.DTO;

public class RecipeCommentDTO
{
    public string? CommentId { get; set; }
    public string? RecipeId { get; set; }
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public string? Comment { get; set; }
    public string? Rating { get; set; }
    public string? CreatedAt { get; set; }
    public string? ParentCommentId { get; set; }
    public List<RecipeCommentDTO>? Replies { get; set; }
}