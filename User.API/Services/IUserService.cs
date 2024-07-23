
using User.API.Dto.Request;
using User.API.Dto.Response;

namespace User.API.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(int id);
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<UserResponse> CreateUser(UserRequest user);
        Task<UserResponse> UpdateUser(UserRequest user);
        Task DeleteUser(int id);
        Task<IEnumerable<RoleResponse>> GetRole();
        Task<IEnumerable<PermisionResponse>> GetPermision();
    }
}
