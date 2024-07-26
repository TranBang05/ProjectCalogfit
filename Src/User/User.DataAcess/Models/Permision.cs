﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.DataAcess.Models
{
    public class Permision
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public string? Description { get; set; }

        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
