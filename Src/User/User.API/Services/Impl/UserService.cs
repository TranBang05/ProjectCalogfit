using AutoMapper;
using User.API.Dto.Request;
using User.API.Dto.Response;
using User.DataAcess.Repositories;
using User.DataAcess.Models;

namespace User.API.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            var user = _mapper.Map<DataAcess.Models.User>(userRequest);
            var createdUser = await _repository.CreateUser(user);

            if (userRequest.Roles != null && userRequest.Roles.Count > 0)
            {
                foreach (var roleRequest in userRequest.Roles)
                {
                    await _repository.AddRoleToUser(createdUser.UserId, roleRequest.RoleId);

                    if (roleRequest.PermissionIds != null && roleRequest.PermissionIds.Count > 0)
                    {
                        foreach (var permissionId in roleRequest.PermissionIds)
                        {
                            await _repository.AddPermissionToRole(roleRequest.RoleId, permissionId);
                        }
                    }
                }
            }
            var userWithRoles = await _repository.GetUserById(createdUser.UserId);
            var userResponse = _mapper.Map<UserResponse>(userWithRoles);

            return userResponse;
        }

        public Task<DataAcess.Models.User> CreateUser(DataAcess.Models.User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(int id)
        {
            await _repository.DeleteUser(id);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<IEnumerable<PermisionResponse>> GetPermision()
        {
            var p = await _repository.getAllPermision();
            return _mapper.Map<IEnumerable<PermisionResponse>>(p);
        }

        public async Task<IEnumerable<RoleResponse>> GetRole()
        {
            var p = await _repository.getAllRoles();
            return _mapper.Map<IEnumerable<RoleResponse>>(p);
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var user = await _repository.GetUserById(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> UpdateUser(UserRequest userRequest)
        {
            var existingUser = await _repository.GetUserById(userRequest.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            _mapper.Map(userRequest, existingUser);
            await _repository.UpdateUser(existingUser);

            await _repository.DeleteRolesFromUser(existingUser.UserId);
            await _repository.DeletePermissionsFromRoles(existingUser.UserId);

            if (userRequest.Roles != null && userRequest.Roles.Count > 0)
            {
                foreach (var roleRequest in userRequest.Roles)
                {
                    await _repository.AddRoleToUser(existingUser.UserId, roleRequest.RoleId);

                    if (roleRequest.PermissionIds != null && roleRequest.PermissionIds.Count > 0)
                    {
                        foreach (var permissionId in roleRequest.PermissionIds)
                        {
                            await _repository.AddPermissionToRole(roleRequest.RoleId, permissionId);
                        }
                    }
                }
            }

            var updatedUser = await _repository.GetUserById(existingUser.UserId);
            var userResponse = _mapper.Map<UserResponse>(updatedUser);
            return userResponse;
        }

        
    }
}
