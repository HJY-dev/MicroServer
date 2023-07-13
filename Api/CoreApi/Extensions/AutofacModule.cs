using Autofac;
using Autofac.Extras.DynamicProxy;
using CoreApi.Entity;
using CoreApi.Proxy;
using CoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreApi.Extensions
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            //获取所有控制器类型并使用属性注入
            var controllerBaseType = typeof(ControllerBase);
            containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();

            //注入服务
            containerBuilder.RegisterType<UserProxyService>().As<IUserProxyService>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            //注入AOP
            containerBuilder.Register(c => new MyAutofac());

            //注入代理
            containerBuilder.RegisterType<MyAutofac>();
            containerBuilder.RegisterType<UserProxyService>().As<IUserProxyService>()
                .InterceptedBy(typeof(MyAutofac)) //指定具体的Aop模块
                .EnableInterfaceInterceptors() //开启aop拦截
                .InstancePerLifetimeScope(); //Scope周期

            //程序集注入代理
            //containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(type => !type.IsInterface && !type.IsSealed && !type.IsAbstract
            //           && type.Name.EndsWith("ProxyService", StringComparison.OrdinalIgnoreCase))
            //           .AsImplementedInterfaces()
            //           .InstancePerLifetimeScope()
            //           .EnableInterfaceInterceptors()
            //           .InterceptedBy(typeof(MyAutofac));

        }
    }
}
