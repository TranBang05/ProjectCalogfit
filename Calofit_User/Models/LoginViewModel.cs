using System.ComponentModel.DataAnnotations;

namespace Calofit_User.Models
{
    public class LoginViewModel
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
