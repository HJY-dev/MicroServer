using Autofac.Extras.DynamicProxy;
using CoreApi.Entity;
using CoreApi.Proxy;
using System;

namespace CoreApi.Services
{
    public class UserProxyService : IUserProxyService
    {
        public bool AddUser(User user)
        {
            Console.WriteLine("用户添加成功");
            return true;
        }

        public bool DelUser(long id)
        {
            Console.WriteLine("删除添加成功");
            return true;
        }
    }
}
