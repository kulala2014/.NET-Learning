using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadConsole
{
   public static class CancellationTokenSourceDemo
   {
        public static void Do()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationTokenSource cts2 = new CancellationTokenSource();
            CancellationTokenSource cts3 = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("Cancelled 1"));
            cts.Token.Register(() => Console.WriteLine("Cancelled 2"));



            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));
            Console.ReadLine();
            cts.Cancel();

            ThreadPool.QueueUserWorkItem(state => Count1(cts1.Token, 100));
            cts1.CancelAfter(2000);

            CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts2.Token, cts3.Token);
            linkedCts.Token.Register(() => Console.WriteLine("LinkedCts is canceled"));
            linkedCts.CancelAfter(3000);
            Console.WriteLine($"cts2:{cts2.IsCancellationRequested}, cts3: {cts3.IsCancellationRequested}");
        }

        private static void Count(CancellationToken token, int countTo)
        {
            for (int count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break;
                }
                Console.WriteLine($"count:{count}");
                Thread.Sleep(200);
            }
            Console.WriteLine("Count is Done");
        }

        private static void Count1(CancellationToken token, int countTo)
        {
            for (int count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break;
                }
                Console.WriteLine($"count:{count}");
                Thread.Sleep(200);
            }
            Console.WriteLine("Count is Done");
        }
    }
}
