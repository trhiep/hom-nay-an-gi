namespace HomNayAnGiAPI.Models.DTO.Recipe;

public class RecipeIngredientDTO
{
    public int RecipeIngredientId { get; set; }
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public double? Quantity { get; set; }
    public string? Unit { get; set; }
    
    public override string ToString()
    {
        return $"RecipeIngredientId: {RecipeIngredientId}, RecipeId: {RecipeId}, IngredientId: {IngredientId}, Quantity: {Quantity?.ToString() ?? "N/A"}, Unit: {Unit ?? "N/A"}";
    }
}