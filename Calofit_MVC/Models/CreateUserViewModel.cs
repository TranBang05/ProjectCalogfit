namespace Calofit_MVC.Models
{
    public class CreateUserViewModel
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<RoleViewModel>? Roles { get; set; }
        public List<int> SelectedRoleIds { get; set; } = new List<int>();
        public List<int> SelectedPermissionIds { get; set; } = new List<int>();

    }
}
