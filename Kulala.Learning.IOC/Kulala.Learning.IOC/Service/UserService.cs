using Kulala.Learning.IOC.Model;

namespace Kulala.Learning.IOC.Service
{
    public class UserService : IUserService
    {
        private readonly ILogService logService;

        public UserService(ILogService logService)
        {
            this.logService = logService;   
        }
        public void GetUser(int id)
        {
            logService.Info("Geting user info with user id");
        }

        public void Register(User user)
        {
            logService.Info($"Try to register user:{user.Name}-{user.Password}");
        }
    }
}
