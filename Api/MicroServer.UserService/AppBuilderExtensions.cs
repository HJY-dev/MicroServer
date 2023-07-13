using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServer.UserService
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            ConsulClient _client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{configuration["Consul:ServerUrl"]}");
                c.Datacenter = "MicroServer.UserService";
            });

            string ip = configuration["Service:IP"];
            string port = configuration["Service:Port"];
            _client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "UserService-" + Guid.NewGuid(),
                Name = "MicroServer.UserService",
                Address = ip,
                Port = Convert.ToInt32(port),
                Tags = new string[] { string.IsNullOrEmpty(configuration["Service:Tags"]) ? "" : configuration["Service:Tags"] },   //标签
                Check = new AgentServiceCheck()                                                                     //健康检查
                {
                    Interval = TimeSpan.FromSeconds(10),                                                            //每隔多久检测一次
                    HTTP = $"http://{ip}:{port}/api/health",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)                                       //在遇到异常后关闭自身服务通道
                }
            });

            return app;
        }

    }
}
