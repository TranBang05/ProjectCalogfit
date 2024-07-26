namespace Calofit_MVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? Goals { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
    }
}
