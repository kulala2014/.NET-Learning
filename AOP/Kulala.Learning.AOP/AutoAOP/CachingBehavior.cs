using Kulala.Learning.AOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Kulala.Learning.AOP.AutoAOP
{
    class CachingBehavior : BaseBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine(nameof(CachingBehavior));
            if (input.MethodBase.Name.Equals("Register"))
                return input.CreateMethodReturn(new User { Id=123,Name="kulala"});
            return getNext().Invoke(input, getNext);
        }
    }
}
