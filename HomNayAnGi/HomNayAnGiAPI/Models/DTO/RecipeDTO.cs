namespace HomNayAnGiAPI.Models.DTO
{
    public class RecipeDTO
    {
        public string RecipeId { get; set; }
        public string? CategoryName { get; set; }
        public string RecipeName { get; set; } = null!;
        public string? Description { get; set; }
        public string? CookTime { get; set; }
        public string? PrepTime { get; set; }
        public string? Servings { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? UserId { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string IsPublic { get; set; }
    }
}
