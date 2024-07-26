using System.ComponentModel.DataAnnotations;

namespace User.API.Dto.Request
{
    public class AuthRequest
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
