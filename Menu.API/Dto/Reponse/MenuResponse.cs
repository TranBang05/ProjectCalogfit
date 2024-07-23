using Menu.DataAccess.Models;

namespace Menu.API.Dto.Reponse
{
    public class MenuResponse
    {
        public int MenuId { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public MenuType? MenuTypes { get; set; }
        public List<MealResponse> Meals { get; set; }
    }
}
