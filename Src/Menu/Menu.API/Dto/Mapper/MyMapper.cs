using AutoMapper;
using Menu.API.Dto.Reponse;
using Menu.API.Dto.Request;
using Menu.DataAccess.Models;

namespace Menu.API.Dto.Mapper
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            // Ánh xạ từ MenuRequest sang Menu
            CreateMap<MenuRequest, DataAccess.Models.Menu>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MenuTypes, opt => opt.MapFrom(src => src.MenuTypes))
                .ForMember(dest => dest.Date, opt => opt.Ignore()) 
                .ForMember(dest => dest.Meals, opt => opt.Ignore()); 


            CreateMap<DataAccess.Models.Menu, MenuResponse>()
                .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.MenuTypes, opt => opt.MapFrom(src => src.MenuTypes))
                .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals));

           
            CreateMap<Meal, MealResponse>()
                .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.MealId))
                .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => src.MealTypes))
                .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems));

            CreateMap<MenuItem, MenuItemResponse>()
                .ForMember(dest => dest.MenuItemId, opt => opt.MapFrom(src => src.MenuItemId))
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.RecipeId));
        }
    }
}
