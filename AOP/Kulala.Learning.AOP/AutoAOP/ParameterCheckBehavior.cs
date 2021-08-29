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
    class ParameterCheckBehavior : BaseBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine(nameof(ParameterCheckBehavior));
            User user = input.Inputs[0] as User;
            if (user.Name.Contains("-"))
            {
                return input.CreateExceptionMethodReturn(new Exception("用户名不能含有特殊字符'-'"));
            }
            else if (user.Password.Length < 6)
            {
                return input.CreateExceptionMethodReturn(new Exception("密码长度不能小于6位"));
            }
            else
            {
                Console.WriteLine("参数检测无误");
                return getNext().Invoke(input, getNext);
            }
            throw new NotImplementedException();
        }
    }
}
