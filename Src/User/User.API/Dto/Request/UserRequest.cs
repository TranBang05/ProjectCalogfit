namespace User.API.Dto.Request
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<RoleRequest>? Roles { get; set; }
    }
}
