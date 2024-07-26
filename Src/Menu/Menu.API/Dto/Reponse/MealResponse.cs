using Menu.DataAccess.Models;

namespace Menu.API.Dto.Reponse
{
    public class MealResponse
    {
        public int MealId { get; set; }
        public MealType? MealType { get; set; }
        public List<MenuItemResponse>? MenuItems { get; set; }
    }
}
