using MicroServer.Organization.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServer.Organization.Services
{
    public interface IRoleModulePermissionServices
    {
        Task<List<RoleModulePermission>> RoleModuleMaps();
    }
}
