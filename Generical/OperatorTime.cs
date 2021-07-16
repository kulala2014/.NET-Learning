using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Generical
{
    class OperatorTime
    {
        public static void show()
        {
            ValueTypePerfTest();
            ReferenceTypePerfTest();
        }

        private static void ValueTypePerfTest()
        {
            const int count = 100_000_000;

            using (new OperatorTimer("List<int>"))
            {
                var l = new List<int>();
                for (int n =0; n < count; n++)
                {
                    l.Add(n); //不发生装箱
                    int x = l[n]; //不发生装箱
                }
                l = null;//确保进行垃圾回收
            }

            using (new OperatorTimer("ArrayList of int"))
            {
                var a = new ArrayList();
                for (int n = 0; n < count; n++)
                {
                    a.Add(n); //装箱
                    int x = (int)a[n]; //拆箱
                }
                a = null;
            }
        }
        private static void ReferenceTypePerfTest()
        {
            const int count = 100_000_000;

            using (new OperatorTimer("List<string>"))
            {
                var l = new List<string>();
                for (int n = 0; n < count; n++)
                {
                    l.Add("x"); //不发生装箱
                    string x = l[n]; //不发生装箱
                }
                l = null;//确保进行垃圾回收
            }

            using (new OperatorTimer("ArrayList of string"))
            {
                var a = new ArrayList();
                for (int n = 0; n < count; n++)
                {
                    a.Add("X"); //装箱
                    string x = (string)a[n]; //拆箱
                }
                a = null;
            }
        }
    }
    internal sealed class OperatorTimer : IDisposable
    {
        private Stopwatch m_stopwatch;
        private string m_text;
        private int m_collectionCount;

        public OperatorTimer(string text)
        {
            PrepareForOperation();

            m_text = text;
            m_collectionCount = GC.CollectionCount(0);
            m_stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            Console.WriteLine($"{m_stopwatch.Elapsed} GCs={(GC.CollectionCount(0) - m_collectionCount)} {m_text}");
        }

        private void PrepareForOperation()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
