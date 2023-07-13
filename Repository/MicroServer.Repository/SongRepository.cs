using FreeSql;
using MicroServer.Organization.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServer.Repository
{
    public class SongRepository : BaseRepository<RoleModulePermission, int>
    {
        public SongRepository(IFreeSql fsql) : base(fsql, null, null) { }

        //在这里增加 CURD 以外的方法
    }
}
