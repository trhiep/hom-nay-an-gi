using AutoMapper;
using HomNayAnGiAPI.Models;
using HomNayAnGiAPI.Models.DTO.Recipe;

namespace HomNayAnGiAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? "Chưa phân loại" : $"{src.Category.CategoryName}"))
                .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.User == null ? "Hôm Nay Ăn Gì" : $"{src.User.Username}"))
                .ForMember(dest => dest.RecipeMeals, opt => opt.MapFrom(src => string.Join(", ", src.RecipeMeals.Select(rm => rm.Meal.MealName))))
                .ReverseMap();

            CreateMap<RecipeIngredient, RecipeIngredientDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => $"{src.Ingredient.IngredientName}"))
                .ReverseMap();

            CreateMap<NutritionFact, NutritionFactDTO>().ReverseMap();

            CreateMap<RecipeStep, RecipeStepDTO>().ReverseMap();

            CreateMap<StepImage, StepImageDTO>().ReverseMap();


        }
    }
}
