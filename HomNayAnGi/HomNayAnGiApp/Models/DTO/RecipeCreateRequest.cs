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
    public override string ToString()
    {
        return $"RecipeCreateRequest: " +
               $"CategoryId={CategoryId}, " +
               $"RecipeName={RecipeName}, " +
               $"Description={Description ?? "N/A"}, " +
               $"CookTime={CookTime ?? 0} mins, " +
               $"PrepTime={PrepTime ?? 0} mins, " +
               $"Servings={Servings ?? 0}, " +
               $"DifficultyLevel={DifficultyLevel ?? "N/A"}, " +
               $"Username={Username ?? "Anonymous"}, " +
               $"Image={Image ?? "No image"}, " +
               $"Video={Video ?? "No video"}";
    }
}