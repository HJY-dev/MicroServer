using CoreApi.Entity;
using CoreApi.Proxy;
using System;

namespace CoreApi.Services
{
    public class DeptService : IDeptService
    {
        [MyRougamo]
        public bool AddDept(Dept entity)
        {
            Console.WriteLine("部门添加成功");
            return true;
        }
    }
}
