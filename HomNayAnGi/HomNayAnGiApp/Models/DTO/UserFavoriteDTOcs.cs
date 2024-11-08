namespace HomNayAnGiApp.Models.DTO
{
    public class UserFavoriteDTO
    {
        public int UserFavoriteId { get; set; }
        public int RecipeId { get; set; }
        public string CreateByUserName { get; set; }
        public int CreateById { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
