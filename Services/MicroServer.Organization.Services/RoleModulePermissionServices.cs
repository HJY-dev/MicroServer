using MicroServer.Organization.Entities;
using MicroServer.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroServer.Organization.Services
{
    public class RoleModulePermissionServices : IRoleModulePermissionServices
    {
        private readonly SongRepository _songRepository;
        public RoleModulePermissionServices(SongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public Task<List<RoleModulePermission>> RoleModuleMaps()
        {
           return _songRepository.Select.Page(1, 10).ToListAsync();
        }
    }
}
