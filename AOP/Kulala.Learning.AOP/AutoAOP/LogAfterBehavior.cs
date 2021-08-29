using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Kulala.Learning.AOP.AutoAOP
{
    class LogAfterBehavior : BaseBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine(nameof(LogAfterBehavior));
            foreach (var item in input.Inputs)
            {
                Console.WriteLine($"LogAfterBehavior: {item.ToString()}");
            }

            IMethodReturn methodReturn = getNext()(input, getNext);
            Console.WriteLine($"LogAfterBehavior: {methodReturn.ReturnValue}");
            return methodReturn;
        }
    }
}
