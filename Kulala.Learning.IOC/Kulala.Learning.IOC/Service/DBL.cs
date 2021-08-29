using System;

namespace Kulala.Learning.IOC.Service
{
    public class DBL : IDBL
    {
        public void Query()
        {
            Console.WriteLine("Query user info from DB");
        }
    }
}
