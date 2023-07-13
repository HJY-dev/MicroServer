using Autofac;
using CoreApi.Controllers;
using CoreApi.Extensions;
using CoreApi.Services;
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

namespace CoreApi
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
            services.AddScoped<IUserProxyService, UserProxyService>();
            services.AddScoped<IDeptService, DeptService>();

            services.AddSwaggerGen((s) =>
            {
                //Ψһ��ʶ�ĵ���URI�Ѻ�����
                s.SwaggerDoc("swaggerName", new OpenApiInfo()
                {
                    Title = "swagger�������ò���",//���������ı��⡣
                    Version = "5.3.1",//������汾��(����ֱ��д����Swashbuckle.AspNetCore���İ汾��,(��д v1 ��))
                    Description = "������Ϣ",//��Ӧ�ó���ļ��������
                    Contact = new OpenApiContact()//����API����ϵ��Ϣ
                    {
                        Email = "123456789@qq.com",
                        Name = "����",
                        Extensions = null,
                        Url = null
                    },
                    License = new OpenApiLicense()//����API�������Ϣ
                    {
                        Name = "����",
                        Extensions = null,
                        Url = null
                    }
                });

                //�������ע�� 
                //ƴ�����ɵ�XML�ļ�·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //HomeControllerΪ��ǰ�����µ�һ���ࣨ���Զ���һ����ǰӦ�ó����µ�һ���ࣩ[���ڻ�ȡ��������]
                var commentsFileName = typeof(HomeController).Assembly.GetName().Name + ".xml";
                var xmlPath = Path.Combine(basePath, commentsFileName);
                s.IncludeXmlComments(xmlPath);

                s.DocInclusionPredicate((docName, description) => true);

            });
            
            services.AddControllers();
        }

        /// <summary>
        /// Autofac
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        /// <summary>
        /// Autofac ����
        /// </summary>
        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //ʹ��swagger�м��,���ṩUI����
            app.UseSwagger();
            app.UseSwaggerUI((s) =>
            {
                //ע�⣺/swagger/Ψһ��ʶ�ĵ���URI�Ѻ�����/swagger.josn   
                s.SwaggerEndpoint("/swagger/swaggerName/swagger.json", "CoreApi");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
