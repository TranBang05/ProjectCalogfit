namespace Calofit_MVC.Models
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<PermissionViewModel>? Permissions { get; set; }
    }
}
