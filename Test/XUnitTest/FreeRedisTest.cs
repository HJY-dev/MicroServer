using FreeRedis;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTest
{
    public class FreeRedisTest
    {
        public static RedisClient cli = new RedisClient("127.0.0.1:6379,password=123,defaultDatabase=13");
        private readonly IConfiguration _configuration;

        public FreeRedisTest(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public FreeRedisTest()
        {

        }

        [Fact]
        public void Test1()
        {

            var userName = _configuration["UserName"];
            Assert.Equal("Alice", userName);
        }

    }
}
