using Kulala.Learning.IOC.kulala.InjectionFlag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kulala.Learning.IOC.kulala
{
    public class TestKulalaContainer
    {
        public static void Show()
        {
            IKulalaContainer container = new KulalaContainer();
            container.RegisterType<IKulala, Kulala>(LifeCycleType.Singleton);
            //container.RegisterType<IKulala, KulalaChild>();
            container.RegisterType<ITest, Test>(LifeCycleType.Transient);
            container.RegisterType<ITest1, Test1>(LifeCycleType.PerThread);
            container.RegisterType<ITest2, Test2>();
            container.RegisterType<ITest3, Test3>();
            IKulala kulala = container.Resolve<IKulala>();
            IKulala kulala1 = container.Resolve<IKulala>();
            Console.WriteLine($"{nameof(kulala)} and {nameof(kulala1)} are the same instance?{object.ReferenceEquals(kulala, kulala1)}");

            ITest kulala2 = container.Resolve<ITest>();
            ITest kulala3 = container.Resolve<ITest>();
            Console.WriteLine($"{nameof(kulala2)} and {nameof(kulala3)} are the same instance?{object.ReferenceEquals(kulala2, kulala3)}");
            ITest1 pad1 = null;
            ITest1 pad2 = null;
            ITest1 pad3 = null;

            Task.Run(() =>
                            {
                                pad1 = container.Resolve<ITest1>();
                                Console.WriteLine($"pad1由线程id={Thread.CurrentThread.ManagedThreadId}");
                            });

            Task.Run(() =>
            {
                pad2 = container.Resolve<ITest1>();
                Console.WriteLine($"pad2由线程id={Thread.CurrentThread.ManagedThreadId}");
            });
            Thread.Sleep(1000);
            Console.WriteLine($"object.ReferenceEquals(pad1, pad2)={object.ReferenceEquals(pad1, pad2)}");
        }



        interface IKulala
        {
            void Show();
        }

        class Kulala : IKulala
        {
            private ITest test;

            //[Injection]
            public Kulala()
            {
                
            }
            public Kulala(ITest test)
            {
                this.test = test;
            }
            public void Show()
            {
                Console.WriteLine("Testing with my own IOC container");
                test?.show();
            }
        }

        //private class KulalaChild : Kulala
        //{

        //}

        interface ITest
        {
            void show();
        }
        class Test : ITest
        {
            public Test(ITest1 test)
            {

            }
            public void show()
            {
                Console.WriteLine("Testing injection method"); ;
            }
        }
        interface ITest1
        {
            void show();
        }
        class Test1 : ITest1
        {
            public Test1(ITest2 test)
            {

            }
            public void show()
            {
                Console.WriteLine("Testing injection method"); ;
            }
        }
        interface ITest2
        {
            void show();
        }
        class Test2 : ITest2
        {
            public Test2(ITest3 test)
            {
                
            }
            public void show()
            {
                Console.WriteLine("Testing injection method"); ;
            }
        }
        interface ITest3
        {
            void show();
        }
        class Test3 : ITest3
        {
            public void show()
            {
                Console.WriteLine("Testing injection method"); ;
            }
        }
    }
}
