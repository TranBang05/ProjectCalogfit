namespace User.API.Dto.Request
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public List<int>? PermissionIds { get; set; }
    }
}
