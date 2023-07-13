﻿using Castle.DynamicProxy;
using System;

namespace CoreApi.Proxy
{
    public class MyAutofac : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //执行原有方法之前
            Console.WriteLine("增加用户前执行业务");

            //执行原有方法
            invocation.Proceed();

            //执行原有方法之后
            Console.WriteLine("增加用户后执行业务");
        }
    }
}
