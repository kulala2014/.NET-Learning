using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Event
{
   public class NewMailEventArgs : EventArgs
    {
        private string m_from, m_to, m_subject;

        public NewMailEventArgs(string from, string to, string subject) => (m_from, m_to,m_subject) = (from, to, subject);
        
        public string From
        {
            get => m_from;
        }
        public string To
        {
            get => m_to;
        }
        public string Subject
        {
            get => m_subject;
        }

    }
}
