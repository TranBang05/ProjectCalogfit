using AutoMapper;
using Recipe.API.Dto.Request;
using Recipe.API.Dto.Response;
using Recipe.DataAcess.Models;

namespace Recipe.API.Dto.Mapper
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap< DataAcess.Models.Recipe, RecipeResponse>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<RecipeRequest, DataAcess.Models.Recipe>()
               .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
               .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps));
               //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<IngredientRequest, Ingredient>();
            CreateMap<StepRequest, Step>();
            CreateMap<Ingredient, IngredientResponse>();
            CreateMap<Step, StepResponse>();
            CreateMap<ReviewRequest, Review>();
            CreateMap<Review, ReviewResponse>();
            CreateMap<RecipeCategoryRequest, RecipeCategory>();

        }
    }
}
