using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk
{
    enum Priority
    {
        Low,
        High
    }

    class Ticket
    {
        public string m_description;
        public Priority m_prio;
        public Ticket Next;
        public Ticket(string desc, Priority prio)
        {
            m_description = desc;
            m_prio = prio;
        }

        public void Print()
        {
            Console.WriteLine("{0}\nPriority:{1}", m_description, m_prio == Priority.High ? "High" : "Low");
        }
    }
}
