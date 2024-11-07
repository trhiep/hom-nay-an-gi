namespace HomNayAnGiAPP.Models.DTO;

public class RecipeCreateRequest
{
    public int? CategoryId { get; set; }
    public string RecipeName { get; set; } = null!;
    public string? Description { get; set; }
    public int? CookTime { get; set; }
    public int? PrepTime { get; set; }
    public int? Servings { get; set; }
    public string? DifficultyLevel { get; set; }
    public string? Username { get; set; }
    public string? Image { get; set; }
    public string? Video { get; set; }
}