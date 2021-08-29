using Kulala.Learning.AOP.IContract;
using Kulala.Learning.AOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulala.Learning.AOP.StaticAOP
{
    public class ProxyUserProcess: IUserProcess
    {
        IUserProcess userProcess;

        public ProxyUserProcess(IUserProcess userProcess)
        {
            this.userProcess = userProcess;
        }

        public void Register(User user)
        {
            Console.WriteLine("Proxy: before register, I want to check your information");
            userProcess.Register(user);
            Console.WriteLine("Proxy: After register, I want to save your information");
        }
    }
}
