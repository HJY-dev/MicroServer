using MicroServer.Common.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTest.Model;

namespace XUnitTest
{
    public class MongoTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoService _mongoService;
        public MongoTest(IConfiguration configuration, IMongoService mongoService)
        {
            _configuration = configuration;
            _mongoService = mongoService;
        }

        [Fact]
        public void Test1()
        {
            var list = new List<FilterDefinition<user_mongo>>();
            list.Add(Builders<user_mongo>.Filter.Exists("user_id", true));
            var filter = Builders<user_mongo>.Filter.And(list);

            var userName = _configuration["UserName"];
            Assert.Equal("Alice", userName);
        }
    }
}
