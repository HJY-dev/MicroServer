using MicroServer.Common.Configuration;
using MicroServer.Common.InfluxDb;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using XUnitTest.Model;

namespace XUnitTest
{
    public class InfluxDbTest
    {
        private readonly IConfiguration _configuration;
        public InfluxDbTest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Fact]
        public void Test1()
        {
            string symbol = "BTCUSDT";
            int limit = 30;

            //创建influxDB
            var a = InfluxDbRepository.DefaultInstance.CreateDb("dbName2").ConfigureAwait(false).GetAwaiter().GetResult();

            //插入数据
            var b = InfluxDbRepository.DefaultInstance.InsertAsync(new Kline { open = 2, close = 2, high = 2, low = 2, val = 1, vol = 2 }, "Test", "tb1");

            //获取数据
            var dbName = ConfigurationManager.Appsettings.GetSection("influxdb:dbName2").Value;
            var query = $"select * from k_{symbol.ToUpper()}_1m limit {limit}  order by time asc";
            var list = InfluxDbRepository.DefaultInstance.GetDataListAsync<Kline>(query, dbName).Result;

            var userName = _configuration["UserName"];
            Assert.Equal("Alice", userName);
        }
    }
}
