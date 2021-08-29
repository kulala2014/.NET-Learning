using Kulala.Learning.AOP.AutoAOP;
using Kulala.Learning.AOP.Model;
using Kulala.Learning.AOP.Service;
using Kulala.Learning.AOP.StaticAOP;
using System;

namespace Kulala.Learning.AOP
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User 
            {
                Name = "Kulala",
                Id = 9527,
                Password = "Qq123!@#"
            };

            //Unity AOP
            UnityAOP.Show();

            //装饰器模式实现静态AOP
            UserProcess userProcess = new UserProcess();
            BeforeUserProcess beforeUserProcess = new BeforeUserProcess();
            AfterUserProcess afterUserProcess = new AfterUserProcess();

            beforeUserProcess.SetDecorateUserProcess(userProcess);
            afterUserProcess.SetDecorateUserProcess(beforeUserProcess);

            afterUserProcess.Register(user);


            //代理模式实现静态AOP
            ProxyUserProcess proxyUserProcess = new ProxyUserProcess(userProcess);
            proxyUserProcess.Register(user);


            Console.ReadLine();
        }
    }
}
