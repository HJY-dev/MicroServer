using MicroServer.Common.Configuration;
using MicroServer.Common.Helper;
using MicroServer.Common.ServiceExtensions.Policys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServer.Common.ServiceExtensions
{
    /// <summary>
    /// Ids4权限 认证服务
    /// </summary>
    public static class Authentication_Ids4Setup
    {
        public static void AddAuthentication_Ids4Setup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            // 添加Identityserver4认证
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
            .AddJwtBearer(options =>
            {
                options.Authority = ConfigurationManager.Appsettings.GetSection("Startup:IdentityServer4:AuthorizationUrl").Value.ObjToString();
                options.RequireHttpsMetadata = false;
                options.Audience = ConfigurationManager.Appsettings.GetSection("Startup:IdentityServer4:ApiName").Value.ObjToString();
            })
            .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });
        }
    }
}
