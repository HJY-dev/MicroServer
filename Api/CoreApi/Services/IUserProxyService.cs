using CoreApi.Entity;

namespace CoreApi.Services
{
    public interface IUserProxyService
    {
        bool AddUser(User user);
        bool DelUser(long id);
    }
}
