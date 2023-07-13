using MicroServer.Common.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTest.Model;

namespace XUnitTest
{
    public class ElasticsearchTest
    {
        private readonly IConfiguration _configuration;
        private readonly ElasticClient _client;
        

        public ElasticsearchTest(IConfiguration configuration, IEsClientProvider clientProvider)
        {
            _configuration = configuration;
            _client = clientProvider.GetClient();
        }

        [Fact]
        public void Test1()
        {
            Post post = new Post();
            var result = _client.IndexDocument(post);

            var userName = _configuration["UserName"];
            Assert.Equal("Alice", userName);
        }

        [Fact]
        public void Test2()
        {
            string type = "";
            var result = _client.Search<Post>(s => s
                .From(0)
                .Size(10)
                .Query(q => q.Match(m => m.Field(f => f.Type).Query(type)))).Documents;

            var userName = _configuration["UserName"];
            Assert.Equal("Alice", userName);
        }
    }
}
