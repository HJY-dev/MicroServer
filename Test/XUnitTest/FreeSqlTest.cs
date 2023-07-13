using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTest.Model;

namespace XUnitTest
{
    public class FreeSqlTest
    {
        private readonly IConfiguration _configuration;
        private readonly IFreeSql<MallContext> _freeSql;

        public FreeSqlTest(IConfiguration configuration, IFreeSql<MallContext> freeSql)
        {
            _configuration = configuration;
            _freeSql = freeSql;
        }

        [Fact]
        public void Test1()
        {
            var result = _freeSql.Select<AspNetUsers>().ToList();
            
            var userName = _configuration["UserName"];
            Assert.Equal("Alice", userName);
        }
    }
}
