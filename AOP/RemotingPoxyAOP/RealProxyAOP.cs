using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPoxyAOP
{
    /// <summary>
    /// 使用.net remoting/RealProxy实现动态代理
    /// 局限在业务类必须是继承自MarshalByRefObject类型
    /// </summary>
    class RealProxyAOP
    {
        public static void show()
        {
            UserProcess process = new UserProcess();
            process.Register();

            UserProcess userProcess = TransparentProxy.Create<UserProcess>();

            userProcess.Register();
        }


        public class MyRealProxy<T> : RealProxy
        {
            private T target;

            public MyRealProxy(T target) : base(typeof(T))
            {
                this.target = target;
            }

            public override IMessage Invoke(IMessage msg)
            {
                BeforeProceede(msg);
                IMethodCallMessage callMessage =  (IMethodCallMessage)msg;
                object returnValue = callMessage.MethodBase.Invoke(this.target, callMessage.Args);
                AfterProceede(msg);

                return new ReturnMessage(returnValue, new object[0], 0, null, callMessage);
            }

            public void BeforeProceede(IMessage msg)
            {
                Console.WriteLine("方法执行前可以加入的逻辑");
            }
            public void AfterProceede(IMessage msg)
            {
                Console.WriteLine("方法执行后可以加入的逻辑");
            }
        }

        public static class TransparentProxy
        {
            public static T Create<T>()
            {
                T instance = Activator.CreateInstance<T>();
                MyRealProxy<T> realProxy = new MyRealProxy<T>(instance);
                T transparentProxy = (T)realProxy.GetTransparentProxy();
                return transparentProxy;
            }

        }

        public class UserProcess : MarshalByRefObject, IUserProcess
        {
            public void Register()
            {
                Console.WriteLine("User has registed successfully.");
            }
        }
        }
    }
