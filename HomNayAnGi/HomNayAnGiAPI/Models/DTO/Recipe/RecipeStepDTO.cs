namespace HomNayAnGiAPI.Models.DTO.Recipe;

public class RecipeStepDTO
{
    public int StepId { get; set; }
    public int? RecipeId { get; set; }
    public int? StepNumber { get; set; }
    public string? Instruction { get; set; }
}