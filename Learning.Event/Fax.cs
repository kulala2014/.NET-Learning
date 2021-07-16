using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Event
{
    public class Fax
    {
        public Fax(MailManager mailManager)
        {
            mailManager.NewMail += FaxMsg;
        }

        public void FaxMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine("Fax message:");
            Console.WriteLine($"From = {e.From}, To = {e.To}, Subject = {e.Subject}");
        }

        public void Unregister(MailManager mailManager)
        {
            mailManager.NewMail -= FaxMsg;
        }
    }
}
