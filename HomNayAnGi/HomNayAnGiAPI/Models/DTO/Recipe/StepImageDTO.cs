namespace HomNayAnGiAPI.Models.DTO.Recipe
{
    public class StepImageDTO
    {
        public int StepImageId { get; set; }
        public int StepId { get; set; }
        public string ImageLink { get; set; } = null!;
    }
}
