namespace HomNayAnGiAPI.Models.DTO
{
    public class RecipeDTO
    {
      public int RecipeId { get; set; }
        public string? CategoryName { get; set; }
        public string RecipeName { get; set; } = null!;
        public string? Description { get; set; }
        public int? CookTime { get; set; }
        public int? PrepTime { get; set; }
        public int? Servings { get; set; }
        public string? DifficultyLevel { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public int IsPublic { get; set; }
    }
}
