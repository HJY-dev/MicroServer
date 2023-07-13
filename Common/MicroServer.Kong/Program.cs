using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServer.Kong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var config = new ConfigurationBuilder().AddCommandLine(args).Build();
                    string ip = config["ip"] ?? "*";
                    string port = config["port"] ?? "9102";
                    webBuilder
                     .UseUrls($"http://{ip}:{port}")
                     .UseKestrel()
                     .UseStartup<Startup>();
                });
    }
}
