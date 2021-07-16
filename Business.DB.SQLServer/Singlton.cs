using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DB.SQLServer
{
   class Singlton
    {
        private static Singlton singlton;
        private static Object o = new object();

        private Singlton()
        {

        }

        public static Singlton GetSinglton()
        {
            if (singlton == null)
            {
                lock (o)
                {
                    if (singlton == null)
                    {
                        singlton = new Singlton();
                    }
                }
            }
            return singlton;
        }
    }

    class Singlton1
    {
        private static readonly Singlton1 singlton = new Singlton1();
        private static Object o = new object();

        private Singlton1()
        {

        }

        public static Singlton1 GetSinglton()
        {
            return singlton;
        }
    }
}
