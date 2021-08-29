using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Kulala.Learning.IOC.Model;
using Kulala.Learning.IOC.Service;

namespace Kulala.Learning.IOC.IOC
{
    public class AutofacIOC
    {
        public static void Show()
        {
            Console.WriteLine("*************AutofacIOC*******************");
            var containerBuilder = new ContainerBuilder();//容器建造者
            containerBuilder.RegisterType<LogService>();//注册对象
            containerBuilder.RegisterInstance(new User { Id=123,Name="kulala", Password="1223344"});//注册实例
            containerBuilder.RegisterGeneric(typeof(List<>));
            containerBuilder.Register(c => new User { Id = 124563, Name = "Clyde", Password = "1223344" });//Lambda表达式创建
            containerBuilder.RegisterInstance(SingleTon.GetInstance()).ExternallyOwned();//单例托管

            containerBuilder.RegisterType<UserService>().As<IUserService>();
            containerBuilder.RegisterType<GroupService>().As<IGroupService>();
            containerBuilder.RegisterType<DBL>().As<IDBL>();
            containerBuilder.RegisterType<LogService>().As<ILogService>();

            var container = containerBuilder.Build();//创建容器完毕

            ILogService logService = container.Resolve<ILogService>();
            logService.Info("AutofacIOC");

            IUserService userService = container.Resolve<IUserService>();
            var user = container.Resolve<User>();
            userService.Register(user);



            Console.WriteLine("*************AutofacIOC*******************");
            Console.WriteLine();
        }

        public class SingleTon
        {
            private static readonly SingleTon singleTon = new SingleTon();

            private SingleTon()
            {
                
            }

            public static SingleTon GetInstance() => singleTon;
        }
    }
}
