using Kulala.Learning.IOC.Model;

namespace Kulala.Learning.IOC.Service
{
    public interface IUserService
    {
        void Register(User user);
        void GetUser(int id);
    }
}
