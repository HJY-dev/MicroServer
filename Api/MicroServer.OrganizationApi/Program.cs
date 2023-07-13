using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Loki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServer.OrganizationApi
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
                    string port = config["port"] ?? "9110";
                    webBuilder
                     .UseUrls($"http://{ip}:{port}")
                     .UseKestrel()
                     .UseStartup<Startup>();
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();

                    var useLokiLogging = context.Configuration.GetValue<bool>("UseLokiLogging");
                    if (useLokiLogging)
                    {
                        string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}Logs\{LogEvent}\log.log";
                        string SerilogOutputTemplate = "{NewLine}{NewLine}Date£º{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel£º{Level}{NewLine}Message£º{Message}{NewLine}{Exception}" + new string('-', 50);

                        LokiCredentials credentials = new BasicAuthCredentials("http://192.168.56.145:3100", "admin", "admin");//192.168.56.143  localhost
                        var log = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .Enrich.FromLogContext()
                                .Enrich.WithProperty("app", context.HostingEnvironment.ApplicationName)
                                .Enrich.WithProperty("env", context.HostingEnvironment.EnvironmentName)
                                .WriteTo.LokiHttp(credentials)
                                .WriteTo.Console(new RenderedCompactJsonFormatter())
                                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                                .CreateLogger();

                        builder.AddSerilog(log);
                    }
                })
            ;
    }
}
