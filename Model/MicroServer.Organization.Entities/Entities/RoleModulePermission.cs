using MicroServer.Organization.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServer.Organization.Entities
{
    public class RoleModulePermission
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public Modules Module { get; set; }

        public Role Role { get; set; }

        public Permission Permission { get; set; }
    }
}
