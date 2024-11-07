namespace HomNayAnGiApp.Models.DTO
{
    public class RecipeCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? CreatedBy { get; set; }
    }
}
