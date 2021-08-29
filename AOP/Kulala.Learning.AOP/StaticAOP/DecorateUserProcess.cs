using Kulala.Learning.AOP.IContract;
using Kulala.Learning.AOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulala.Learning.AOP
{
    public abstract class DecorateUserProcess : IUserProcess
    {
        private IUserProcess userProcess;

        public void SetDecorateUserProcess(IUserProcess userProcess)
        {
            this.userProcess = userProcess;
        }

        public virtual void Register(User user)
        {
            userProcess.Register(user);
        }
    }

    public class BeforeUserProcess : DecorateUserProcess
    {

        public override void Register(User user)
        {
            Decorate();
            base.Register(user);
        }

        public void Decorate()
        {
            Console.WriteLine("Before user doing register, he/she must confirm his/her personal info");
        }
    }

    public class AfterUserProcess : DecorateUserProcess
    {

        public override void Register(User user)
        {
            base.Register(user);
            Decorate();
        }

        public void Decorate()
        {
            Console.WriteLine("After user doing register, he/she must remember personal info");
        }
    }
}
