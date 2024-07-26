using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using User.API.Dto.Request;
using User.API.Dto.Response;
using User.DataAcess.Models;

namespace User.API.Dto.Mapper
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap<DataAcess.Models.User, UserResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(sr => sr.Role.RoleName).ToList()))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.UserRoles.SelectMany(sr => sr.Role.RolePermissions.Select(rp => rp.Permissions.PermissionName)).Distinct().ToList())); ;
            CreateMap<UserRequest, DataAcess.Models.User>();

            CreateMap<UserRequest, DataAcess.Models.User>()
               .ForMember(dest => dest.UserRoles, opt => opt.Ignore());
              
            CreateMap<RoleRequest,UserRole >()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<Role, RoleResponse>();
            CreateMap<Permision, PermisionResponse>();

        }
    }
}
