using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadConsole
{
    public static class SuppressFlowDemo
    {
        public static void Show()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Run(() => { Sum(cts.Token, 1000000); }, cts.Token);

            Thread.Sleep(10000);
            cts.Cancel();
        }

        private static void Sum(CancellationToken ct, int n)
        {
            int sum = 0;
            for (;n>0;n--)
            {
                ct.ThrowIfCancellationRequested();
                sum += n;
                Console.WriteLine(sum);


            }
            Thread.Sleep(2000);
            Console.WriteLine(sum);
        }
    }
}
