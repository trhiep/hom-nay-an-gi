namespace HomNayAnGiApp.Models.DTO
{
    public class RecipeUpdateRequest
    {
        public int RecipeId { get; set; }
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
            return $"Recipe ID: {RecipeId}, " +
                   $"Category ID: {CategoryId ?? -1}, " +
                   $"Recipe Name: {RecipeName}, " +
                   $"Description: {Description ?? "N/A"}, " +
                   $"Cook Time: {CookTime ?? -1} mins, " +
                   $"Prep Time: {PrepTime ?? -1} mins, " +
                   $"Servings: {Servings ?? -1}, " +
                   $"Difficulty: {DifficultyLevel ?? "N/A"}, " +
                   $"Username: {Username ?? "N/A"}, " +
                   $"Image: {Image ?? "N/A"}, " +
                   $"Video: {Video ?? "N/A"}";
        }
    }

}
