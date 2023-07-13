using MediatR;

namespace MediatRApi.Event
{
    public class AddUserSuccessEvent:INotification
    {
        public string UserId { get; set;}
        public string UserName { get; set;}
    }
}
