using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Event
{
    public class Pager
    {
        public Pager(MailManager mailManager) => mailManager.NewMail += PagerMsg;

        private void PagerMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine("Pager message: ");
            Console.WriteLine($"From = {e.From}, To = {e.To}, Subject = {e.Subject}");
        }

        public void Unregister(MailManager mailManager) => mailManager.NewMail -= PagerMsg;
    }
}
