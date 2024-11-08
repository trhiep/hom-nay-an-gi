using AutoMapper;
using HomNayAnGiApp.Models.DTO;
using HomNayAnGiAPP.Models.DTO;

namespace HomNayAnGiApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecipeUpdateRequest, RecipeDTO>().ReverseMap();
        }
    }
}
