namespace HomNayAnGiAPI.Models.DTO.Recipe;

public class RecipeStepDTO
{
    public RecipeStepDTO()
    {
        StepImages = new HashSet<StepImageDTO>();
    }
    public int StepId { get; set; }
    public int? RecipeId { get; set; }
    public int? StepNumber { get; set; }
    public string? Instruction { get; set; }
    public virtual ICollection<StepImageDTO> StepImages { get; set; }
}