using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.DataAcess.Models
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

        public ICollection<UserRole>? UserRoles { get; set; }
        
    }
}
