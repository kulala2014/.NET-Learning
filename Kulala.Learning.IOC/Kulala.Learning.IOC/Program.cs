using Kulala.Learning.IOC.IOC;
using System;

namespace Kulala.Learning.IOC
{
    class Program
    {
        static void Main()
        {
            UnityIOC.Show();

            NInjectIOC.Show();


            AutofacIOC.Show();
            Console.Read();
        }
    }
}
