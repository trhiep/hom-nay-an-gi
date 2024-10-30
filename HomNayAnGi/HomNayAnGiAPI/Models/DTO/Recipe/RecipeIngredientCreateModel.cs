namespace HomNayAnGiAPI.Models.DTO.Recipe;

public class RecipeIngredientCreateModel
{
    public int RecipeId { get; set; }
    public List<RecipeIngredientDTO> RecipeIngredients { get; set; }
    
    public override string ToString()
    {
        var ingredientsDetails = RecipeIngredients != null 
            ? string.Join(", ", RecipeIngredients.Select(i => 
                $"[RecipeIngredientId: {i.RecipeIngredientId}, RecipeId: {i.RecipeId}, IngredientId: {i.IngredientId}, Quantity: {i.Quantity}, Unit: {i.Unit}]"))
            : "No ingredients";

        return $"RecipeId: {RecipeId}, RecipeIngredients: {ingredientsDetails}";
    }
}