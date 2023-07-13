using MicroServer.Common.GlobalVar;
using MicroServer.Common.Helper;
using MicroServer.Common.ServiceExtensions;
using MicroServer.Organization.Services;
using MicroServer.OrganizationApi.Middlewares;
using MicroServer.OrganizationApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MicroServer.Repository;
using FreeSql;
using MicroServer.OrganizationApi.DbContext;
using MicroServer.Common.Configuration;
using System.Diagnostics;

namespace MicroServer.OrganizationApi
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
            Permissions.IsUseIds4 = Configuration.GetSection("Startup:IdentityServer4:Enabled").ObjToBool();

            // 确保从认证中心返回的ClaimType不被更改，不使用Map映射
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // 授权+认证 (jwt or ids4)
            services.AddAuthorizationSetup();
            if (Permissions.IsUseIds4)
            {
                services.AddAuthentication_Ids4Setup();
            }
            else
            {
                services.AddAuthentication_JWTSetup();
            }
            

            services.AddSwaggerSetup();

            #region 注入freesql

            var mallSql = new FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.PostgreSQL,ConfigurationManager.Appsettings.GetSection("ConnectionStrings:PgSql").Value)
                .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                .Build<MallContext>();
            services.AddSingleton(mallSql);

            services.AddScoped(typeof(BaseRepository<,>), typeof(SongRepository));
            #endregion


            services.AddScoped<IRoleModulePermissionServices, RoleModulePermissionServices>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 封装Swagger展示
            app.UseSwaggerMildd();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
