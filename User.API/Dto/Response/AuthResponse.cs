namespace User.API.Dto.Response
{
    public class AuthResponse
    {
        public bool Successful { get; set; }
        
        public string? Token { get; set; }
        public List<string>? Roles { get; set; }
    }
}
