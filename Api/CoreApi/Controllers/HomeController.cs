using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using CoreApi.Entity;
using CoreApi.Proxy;
using CoreApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserProxyService _userService;
        private readonly IDeptService _deptService;
        private readonly IServiceProvider _app;

        public HomeController(ILogger<HomeController> logger, IUserProxyService userService, IDeptService deptService, IServiceProvider app)
        {
            _logger = logger;
            _userService = userService;
            _deptService = deptService;
            _app = app;
        }

        /// <summary>
        /// 依赖注入 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddUser(User model)
        {
            var result = false;

            #region 方法一
            var _container = _app.GetAutofacRoot();
            using (var scope = _container.BeginLifetimeScope())
            {
                var dog = scope.Resolve<IUserProxyService>();
                result = dog.AddUser(model);
            }
            #endregion

            #region 方法二
            result = _userService.AddUser(model);
            #endregion

            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DelUser(long id)
        {
            return _userService.DelUser(id);
        }

        /// <summary>
        /// Reflection动态代理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddUserProxy(User model)
        {
            IUserProxyService userService1 = DispatchProxy.Create<IUserProxyService, MyProxy>();
            ((MyProxy)userService1).TargetClass = new UserProxyService();
            var result =  userService1.AddUser(model);
            return result;
        }

        /// <summary>
        /// Castle动态代理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddUserCastleProxy(User model)
        {
            //先实例化一个代理类生成器
            ProxyGenerator generator = new ProxyGenerator();
            //通过代理类生成器创建
            var u = generator.CreateInterfaceProxyWithTarget<IUserProxyService>(new UserProxyService(), new MyIntercept());
            var result = u.AddUser(model);
            return result;
        }

        /// <summary>
        /// Autofac动态代理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddUserAutofacProxy(User model)
        {
            return _userService.AddUser(model);
        }

        /// <summary>
        /// 肉夹馍动态代理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddDept(Dept model)
        {
            return _deptService.AddDept(model);
        }

    }
}
