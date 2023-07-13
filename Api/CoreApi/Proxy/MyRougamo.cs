using System;
using Rougamo;
using Rougamo.Context;

namespace CoreApi.Proxy
{
    public class MyRougamo : MoAttribute
    {
        public override void OnEntry(MethodContext context)
        {
            // 从context对象中能取到包括入参、类实例、方法描述等信息
            Console.WriteLine("方法执行前");
        }

        public override void OnException(MethodContext context)
        {
            Console.WriteLine("方法执行异常", context.Exception);
        }

        public override void OnSuccess(MethodContext context)
        {
            Console.WriteLine("方法执行成功后");
        }

        public override void OnExit(MethodContext context)
        {
            Console.WriteLine("方法退出时，不论方法执行成功还是异常，都会执行");
        }
    }
}
