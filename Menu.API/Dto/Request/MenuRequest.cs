using Menu.DataAccess.Models;

namespace Menu.API.Dto.Request
{
    public class MenuRequest
    {
        public string? Name { get; set; } 
        public int UserId { get; set; } 
        public MenuType? MenuTypes { get; set; } 

       
    }
}
