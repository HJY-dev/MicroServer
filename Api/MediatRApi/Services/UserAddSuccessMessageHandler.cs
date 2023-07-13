using MediatR;
using MediatRApi.Event;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRApi.Services
{
    public class UserAddSuccessMessageHandler : INotificationHandler<AddUserSuccessEvent>
    {
        public Task Handle(AddUserSuccessEvent notification, CancellationToken cancellationToken)
        {
            //发送短信
            Console.WriteLine($"发送短信：{notification.UserName} 注册成功");

            return Task.CompletedTask;
        }
    }
}
