using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Kulala.Learning.AOP.AutoAOP
{
    public class LogBeforeBehavior : BaseBehavior
    {

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine(nameof(LogBeforeBehavior));
            foreach (var item in input.Inputs)
            {
                Console.WriteLine($"loginfo: {item}");
            }
            return getNext().Invoke(input, getNext);
        }
    }
}
