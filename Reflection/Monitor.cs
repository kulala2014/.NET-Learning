using Business.DB.Interface;
using Business.DB.SQLServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Monitor
    {
        private Stopwatch m_stopwatch;
        public void ShowResult()
        {
            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
            for (int i = 0; i < 100_000_000; i++)
            {
                IDBHelper iDBHelper = new SQLServerHelper();
                iDBHelper.Query();
            }
            m_stopwatch.Stop();

            Console.WriteLine($"Common time: {m_stopwatch.ElapsedMilliseconds}");

            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
            for (int i = 0; i < 100_000_000; i++)
            {
                Assembly assembly = Assembly.Load("Business.DB.SQLServer");
                Type type = assembly.GetType("Business.DB.SQLServer.SQLServerHelper");

                object o = Activator.CreateInstance(type);
                IDBHelper dBHelper = o as IDBHelper;
                dBHelper.Query();
            }
            m_stopwatch.Stop();
            Console.WriteLine($"reflection time: {m_stopwatch.ElapsedMilliseconds}");
        }
    }
}
