using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Learning.Event
{
    public class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        public void SimulatorMail(string from, string to, string subject)
        {
            var newMail = new NewMailEventArgs(from, to, subject);
            Console.WriteLine("New Mail coming");
            OnNewMail(newMail);

        }

        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            //EventHandler<NewMailEventArgs> temp = NewMail;
            //编译器这里强制引用事件，读取引用
            e.Raise<NewMailEventArgs>(this, ref NewMail);
        }
    }
}
