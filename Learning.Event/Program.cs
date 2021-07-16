using Learning.Event.ImplementEvent;
using System;

namespace Learning.Event
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Learning Event: ");

            MailManager mailManager = new MailManager();
            Fax fax = new Fax(mailManager);
            Pager pager = new Pager(mailManager);

            mailManager.SimulatorMail("971266892@qq.com", "gaoxiaorui@live.cn", "[Population Tool] Logic enhancement");


            //手写Event
            TryWithImplementEvent twl = new TryWithImplementEvent();
            twl.Foo += (object sender, FooEventArgs e) => Console.WriteLine("Handling Foo Event here");
            twl.SimulateFoo();
            Console.ReadLine();
        }
    }
}
