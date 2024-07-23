namespace User.API.Dto.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Goals { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }

        public List<string>? Roles { get; set; }
        public List<string>? Permissions { get; set; }
    }
}
