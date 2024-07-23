using Menu.API.Dto.Reponse;
using Menu.API.Dto.Request;

namespace Menu.API.Services
{
    public interface IMenuService
    {
        Task<MenuResponse> CreateMenuAsync(MenuRequest request);
        
    }
}
