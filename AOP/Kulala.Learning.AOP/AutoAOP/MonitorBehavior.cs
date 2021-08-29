using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Kulala.Learning.AOP.AutoAOP
{
    class MonitorBehavior : BaseBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine(nameof(MonitorBehavior));
            string methodName = input.MethodBase.Name;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var methodReturn = getNext().Invoke(input, getNext);
            stopwatch.Stop();
            Console.WriteLine($"{this.GetType().Name} 统计方法{methodName} 执行耗时 {stopwatch.ElapsedMilliseconds}ms");

            return methodReturn;
        }
    }
}
