using MediatRApi.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRApi
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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddSwaggerGen((s) =>
            {
                //唯一标识文档的URI友好名称
                s.SwaggerDoc("swaggerName", new OpenApiInfo()
                {
                    Title = "swagger集成配置测试",//（必填）申请的标题。
                    Version = "5.3.1",//（必填）版本号(这里直接写的是Swashbuckle.AspNetCore包的版本号,(有写 v1 的))
                    Description = "描述信息",//对应用程序的简短描述。
                    Contact = new OpenApiContact()//公开API的联系信息
                    {
                        Email = "123456789@qq.com",
                        Name = "张三",
                        Extensions = null,
                        Url = null
                    },
                    License = new OpenApiLicense()//公开API的许可信息
                    {
                        Name = "张三",
                        Extensions = null,
                        Url = null
                    }
                });

                //添加中文注释 
                //拼接生成的XML文件路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //HomeController为当前程序集下的一个类（可自定义一个当前应用程序集下的一个类）[用于获取程序集名称]
                var commentsFileName = typeof(HomeController).Assembly.GetName().Name + ".xml";
                var xmlPath = Path.Combine(basePath, commentsFileName);
                s.IncludeXmlComments(xmlPath);

                s.DocInclusionPredicate((docName, description) => true);

            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //使用swagger中间件,并提供UI界面
            app.UseSwagger();
            app.UseSwaggerUI((s) =>
            {
                //注意：/swagger/唯一标识文档的URI友好名称/swagger.josn   
                s.SwaggerEndpoint("/swagger/swaggerName/swagger.json", "MediatRApi");


            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
