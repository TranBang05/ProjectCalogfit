using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.API.Dto.Response;
using User.DataAcess.Models;
using User.DataAcess.Repositories;

namespace User.API.Services.Impl
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        private readonly IConfiguration _configuration;

        public LoginService(ILoginRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public async Task<AuthResponse> Authenticate(string email, string password)
        {
            var user = await _repository.GetUserAsync(email, password);
            if (user == null)
                return new AuthResponse { Successful = false, Token = null, Roles = null };

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
    };

            var roles = new List<string>(); // Danh sách các vai trò của người dùng

            foreach (var uRole in user.UserRoles)
            {
                var roleName = uRole.Role.RoleName;
                claims.Add(new Claim(ClaimTypes.Role, roleName));
                roles.Add(roleName); 

                var permissions = await _repository.GetPermissionsAsync(uRole.RoleId);
                foreach (var permission in permissions)
                {
                    claims.Add(new Claim("Permission", permission));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: creds);

            return new AuthResponse
            {
                Successful = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = roles // Trả về danh sách các vai trò
            };
        }

    }
}
