namespace Calofit_MVC.Models
{
    public class AuthResponse
    {
        public bool Successful { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}
