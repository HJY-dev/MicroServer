using FreeSql;
using MicroServer.Common.Configuration;
using MicroServer.Common.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using XUnitTest.Model;

namespace XUnitTest
{
    public class Startup
    {
        // 自定义 host 构建
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(builder =>
                {
                    // 注册配置
                    builder.AddInMemoryCollection(new Dictionary<string, string>()
                    {
                        {"UserName", "Alice"}
                    })
                .AddJsonFile("appsettings.json");
                });
        }

        // 支持的形式：
        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IEsClientProvider, EsClientProvider>();

            #region 注入freesql

            var mallSql = new FreeSqlBuilder().UseConnectionString(FreeSql.DataType.PostgreSQL,
            ConfigurationManager.Appsettings.GetSection("ConnectionStrings:PgSql").Value).Build<MallContext>();
            services.AddSingleton(mallSql);

            #endregion


        }

        // 可以添加要用到的方法参数，会自动从注册的服务中获取服务实例，类似于 asp.net core 里 Configure 方法
        public void Configure(IServiceProvider applicationServices)
        {
            // 有一些测试数据要初始化可以放在这里
            // InitData();
        }

    }
}
