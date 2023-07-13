using Kong;
using Kong.Models;
using MicroServer.Kong.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace MicroServer.Kong
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<KongClient>(fat =>
            {
                var options = new KongClientOptions(HttpClientFactory.Create(), this.Configuration["kong:host"]);
                var client = new KongClient(options);
                return client;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, KongClient kongClient)
        {
            UseKong(app, kongClient);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void UseKong(IApplicationBuilder app, KongClient kongClient)
        {
            var upStream = Configuration.GetSection("kong:upstream").Get<UpStream>();
            var target = Configuration.GetSection("kong:target").Get<TargetInfo>();
            var service = Configuration.GetSection("kong:services").Get<ServiceInfo>();
            var route = Configuration.GetSection("kong:routes").Get<RouteInfo>();
            app.UseKong(kongClient, upStream, target, service, route, OnExecuter);
        }
        /// <summary>
        /// Custom HealthChecks
        /// </summary>
        /// <param name="context"></param>
        public void OnExecuter(HttpContext context)
        {
            context.Response.StatusCode = 200;
        }
    }
}
