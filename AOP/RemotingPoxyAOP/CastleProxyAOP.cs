using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace RemotingPoxyAOP
{
    /// <summary>
    /// 使用Castle\DynamicProxy 实现动态代理
    /// 方法必须是虚方法
    /// </summary>
   public class CastleProxyAOP
    {
        public static void Show()
        {
            ProxyGenerator generator = new ProxyGenerator();
            MyInteceptor inteceptor = new MyInteceptor();
            UserProcess userProcess = generator.CreateClassProxy<UserProcess>(inteceptor);
            userProcess.Register();
        }


        public class UserProcess: IUserProcess
        {
            public virtual void Register()
            {
                Console.WriteLine("User has registed successfully.");
            }
        }

        public class MyInteceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                Preproceed(invocation);
                invocation.Proceed();//调用业务方法
                Postproceed(invocation);
            }

            private void Preproceed(IInvocation invocation)
            {
                Console.WriteLine("方法执行前");
            }

            private void Postproceed(IInvocation invocation)
            {
                Console.WriteLine("方法执行后");
            }
        }
    }
}
