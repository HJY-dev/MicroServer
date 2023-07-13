using MediatR;
using MediatRApi.Event;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRApi.Services
{
    public class UserAddSuccessEventEmailHandler:INotificationHandler<AddUserSuccessEvent>
    {
        public Task Handle(AddUserSuccessEvent notification, CancellationToken cancellationToken)
        {
            //发送邮件
            Console.WriteLine($"发送邮件：{notification.UserName} 注册成功");

            return Task.CompletedTask;
        }
    }
}
