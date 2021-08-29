using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //# CancellationTokenSourceDemo.Do();
            Console.WriteLine($"MainThreadId: {Thread.CurrentThread.ManagedThreadId}");
            TestTask();
            Console.WriteLine($"MainThreadId: {Thread.CurrentThread.ManagedThreadId}");
            SuppressFlowDemo.Show();
            Console.ReadLine();
        }

        private async static Task TestTask()
        {
            Console.WriteLine($"***********ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Task task = Task.Run(() => Console.WriteLine($"___________ThreadId: {Thread.CurrentThread.ManagedThreadId}"));
            Thread.Sleep(1000);
            await task;
            Console.WriteLine($"----------------ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
