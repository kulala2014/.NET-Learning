using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kulala.Learning.IOC.Model;
using Kulala.Learning.IOC.Service;
using Ninject;
using Ninject.Modules;

namespace Kulala.Learning.IOC.IOC
{
    public class NInjectIOC
    {
        public static void Show()
        {
            User user = new() { Id = 123, Name = "kulala", Password = "1234" };
            IKernel kernel = new StandardKernel(new ServiceModule());
            Console.WriteLine("**********************NInjectIOC Testing***************************");

            IUserService userService = kernel.Get<IUserService>();
            userService.Register(user);


            Console.WriteLine("**********************NInjectIOC Testing***************************");
        }
    }

    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogService>().To<LogService>();
            Bind<IUserService>().To<UserService>();
            Bind<IGroupService>().To<GroupService>();
            Bind<IDBL>().To<DBL>(); 

        }
    }
}
