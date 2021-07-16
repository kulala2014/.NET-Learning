using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Learning.Event
{
    public static class EventArgsExtention
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> handler)
        {
            //线程安全
            EventHandler<TEventArgs> temp = Volatile.Read(ref handler);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
