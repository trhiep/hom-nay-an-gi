namespace HomNayAnGiAPI.Models.DTO
{
    public class IngredientDTO
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public string? Description { get; set; }
        public int? CreatedBy { get; set; }
    }
}
