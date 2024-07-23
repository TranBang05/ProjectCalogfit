
using AutoMapper;
using Menu.API.Dto.Reponse;
using Menu.API.Dto.Request;
using Menu.DataAccess.Models;
using Menu.DataAccess.Repository;
using Newtonsoft.Json;


namespace Menu.API.Services.Impl
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;
        private readonly IRecipeService _recipeService;
        
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository repository,  IMapper mapper, IRecipeService recipeService)
        {
            _repository = repository;
            _recipeService = recipeService;
            _mapper = mapper;
        }

      

        public async Task<MenuResponse> CreateMenuAsync(MenuRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Menu request cannot be null");
            }
            string categoryName;
            switch (request.Name)
            {
                case "Weightloss":
                    categoryName = "Weightloss";
                    break;
                case "Weightgain":
                    categoryName = "Weightgain";
                    break;
                default:
                    throw new ArgumentException("Invalid menu name", nameof(request.Name));
            }

            var categoryRequest = new RecipeCategoryRequest { CategoryName = categoryName };

            // Lấy danh sách công thức theo category
            var recipes = await _recipeService.GetRecipesByCategory(categoryRequest);

            if (recipes == null || !recipes.Any())
            {
                throw new InvalidOperationException("No recipes found for the specified category");
            }
            var menu = _mapper.Map<DataAccess.Models.Menu>(request);
            menu.Date = DateTime.UtcNow;
            menu.Meals = new List<Meal>();
            int recipeIndex = 0;
            int recipesPerMeal = 1;
            if (request.MenuTypes == MenuType.Daily)
            {
                var mealTypes = new[] { MealType.Breakfast, MealType.Lunch, MealType.Dinner };

                foreach (var mealType in mealTypes)
                {
                    var meal = new Meal
                    {
                        MealTypes = mealType,
                        MenuItems = recipes.Skip(recipeIndex)
                    .Take(recipesPerMeal)
                                 .Select(r => new MenuItem
                                 {
                                     RecipeId = r.RecipeId
                                 }).ToList()
                    };
                    recipeIndex += recipesPerMeal;
                    menu.Meals.Add(meal);
                }
            }
            else if (request.MenuTypes == MenuType.Weekly)
            {
                var mealTypes = new[] { MealType.Breakfast, MealType.Lunch, MealType.Dinner };
                var daysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

                foreach (var day in daysOfWeek)
                {
                    foreach (var mealType in mealTypes)
                    {
                        var meal = new Meal
                        {
                            MealTypes = mealType,
                            MenuItems = recipes
                                .Skip((Array.IndexOf(mealTypes, mealType) + (int)day * mealTypes.Length) * 3)
                                .Take(1)
                                .Select(r => new MenuItem
                                {
                                    RecipeId = r.RecipeId
                                }).ToList()
                        };
                        menu.Meals.Add(meal);
                    }
                }
            }

            _repository.Add(menu);
            await _repository.SaveChangesAsync();
            var menuResponse = _mapper.Map<MenuResponse>(menu);
            return menuResponse;
        }
    }
}