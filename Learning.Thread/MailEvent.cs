using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Thread
{
   public static class MailEvent
    {
        public static void Show()
        {
            var mailManager = new MailManager();
            var faxer = new Faxer(mailManager);
            var pager = new Pager(mailManager);
            mailManager.SimulatorMail("971366892@qq.com", "gaoxiaorui@live.cn", "Confirm Ticket Buy Status");
        }
    }

    public class NewMailEventArgs : EventArgs
    {
        private string _from;
        private string _to;
        private string _subject;
        public NewMailEventArgs(string from, string to, string subject) => (this._from, this._to, this._subject) = (from, to ,subject);

        public string From => _from;
        public string To => _to;
        public string Subject => _subject;
    }


    public class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;
        public MailManager()
        {
            
        }
        public void SimulatorMail(string from, string to, string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(from, to ,subject);
            OnNewMail(this, e);
        }

        protected void OnNewMail(object sender, NewMailEventArgs e)
        {
            e.Raise(this, ref NewMail);
        }
    }

    public static class EventArgsExtention
    {
        public static void Raise(this NewMailEventArgs e, object sender, ref EventHandler<NewMailEventArgs> events)
        {
            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref events);
            temp?.Invoke(sender,e);
        }
    }

    public class Faxer
    {
        public  Faxer(MailManager mailManager)
        {
            mailManager.NewMail += FaxMsg;
        }

        private void FaxMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine("Faxer Mail message: ");
            Console.WriteLine($"Mail details: From-{e.From}, To-{e.To}. Subject: {e.Subject}");
        }

        public void Unregister(MailManager mailManager)
        {
            mailManager.NewMail -= FaxMsg;
        }
    }

    public class Pager
    {
        public Pager(MailManager mailManager)
        {
            mailManager.NewMail += PagerMsg;
        }

        private void PagerMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine("Pager Mail message: ");
            Console.WriteLine($"Mail details: From-{e.From}, To-{e.To}. Subject: {e.Subject}");
        }

        public void Unregister(MailManager mailManager)
        {
            mailManager.NewMail -= PagerMsg;
        }
    }
}
