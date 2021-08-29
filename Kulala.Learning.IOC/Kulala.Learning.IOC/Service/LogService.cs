using System;

namespace Kulala.Learning.IOC
{
    public class LogService : ILogService
    {
        public void Error(string msg)
        {
            Console.WriteLine($"Error: {msg}");
        }

        public void Info(string msg)
        {
            Console.WriteLine($"Info: {msg}");
        }
    }
}
