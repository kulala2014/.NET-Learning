using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPoxyAOP
{
    class Program
    {
        static void Main(string[] args)
        {
            RealProxyAOP.show();
            CastleProxyAOP.Show();
            Console.Read();


        }
    }
}
