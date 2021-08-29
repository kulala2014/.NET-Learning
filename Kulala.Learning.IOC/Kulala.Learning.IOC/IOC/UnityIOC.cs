using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Builder;
using Unity.Registration;
using Unity;
using Kulala.Learning.IOC.Service;
using Kulala.Learning.IOC.Model;
using Unity.Lifetime;
using System.Threading;

namespace Kulala.Learning.IOC.IOC
{
     public class UnityIOC
    {
        public static void Show()
        {
            Console.WriteLine("**********************UnityIOC Testing***************************");
            User user = new() { Id=123,Name="kulala", Password="1234"};

            IUnityContainer container = new UnityContainer();
            container.RegisterType<ILogService, LogService>(new PerThreadLifetimeManager());
            container.RegisterType<IUserService, UserService>();

            ILogService logService = container.Resolve<ILogService>();
            logService.Info("UnityContainer: testing ioc");

            IUserService userService = container.Resolve<IUserService>();
            userService.Register(user);


            container.RegisterType<IDBL, DBL>(new SingletonLifetimeManager());
            container.RegisterType<IGroupService, GroupService>(new TransientLifetimeManager());

            IDBL dBL1 = container.Resolve<IDBL>();
            IDBL dBL2 = container.Resolve<IDBL>();

            Console.WriteLine($"dbl1,db2 are the same instance? {object.ReferenceEquals(dBL1, dBL2)}");

            IGroupService groupService1 = container.Resolve<IGroupService>();
            IGroupService groupService2 =  container.Resolve<IGroupService>();
            Console.WriteLine($"groupService1,groupService2 are the same instance? {object.ReferenceEquals(groupService1, groupService2)}");

            ILogService logService1 = null;
            ILogService logService2 = null;
            ILogService logService3 = null;

            Task.Run(()=> 
            {
                logService1 = container.Resolve<ILogService>();
                logService3 = container.Resolve<ILogService>();
            }).ContinueWith(t=> { });

            Task.Run(() =>
            {
                logService2 = container.Resolve<ILogService>();
            });

            Thread.Sleep(1000);
            Console.WriteLine($"logService1,logService2 are the same instance? {object.ReferenceEquals(logService1, logService2)}");
            Console.WriteLine($"logService1,logService3 are the same instance? {object.ReferenceEquals(logService1, logService3)}");
            Console.WriteLine("**********************UnityIOC Testing***************************");
            Console.WriteLine();
        }
    }
}
