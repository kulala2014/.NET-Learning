using Kulala.Learning.IOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kulala.Learning.IOC.kulala
{
    public class CallContextTesting
    {
        public static void Show()
        {
            //var d1 = new User{ Id=123};
            var t1 = default(object);
            var t2 = default(object);

            Task.WaitAll(
                Task.Run(() =>
                {
                    Console.WriteLine($"Thread :{Thread.CurrentThread.ManagedThreadId}");
                    CallContext.SetData("d1", new User { Id = 123 });
                    t1 = CallContext.GetData("d1");
                }),
                Task.Run(() =>
                {
                    CallContext.SetData("d1", new User { Id = 123 });
                    t2 = CallContext.GetData("d1");
                })
            );
            Thread.Sleep(2000);


            Console.WriteLine(object.ReferenceEquals(t1, t2));
            Console.WriteLine();
        }
    }
}
