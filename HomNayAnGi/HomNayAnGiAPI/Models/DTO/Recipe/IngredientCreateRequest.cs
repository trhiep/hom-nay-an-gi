namespace HomNayAnGiAPI.Models.DTO.Recipe;

public class IngredientCreateRequest
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = null!;
    public string? Description { get; set; }
    public int? CreatedBy { get; set; }
}