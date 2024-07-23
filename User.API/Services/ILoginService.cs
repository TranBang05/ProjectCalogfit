using User.API.Dto.Response;

namespace User.API.Services
{
    public interface ILoginService
    {
        Task<AuthResponse> Authenticate(string email, string password);
    }
}
