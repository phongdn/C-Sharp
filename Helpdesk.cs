//Phong Nguyen
//BIT 143
//Assignment 2.1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk
{
    class Helpdesk
    {
        ListOfTickets HighPriority = new ListOfTickets();
        ListOfTickets LowPriority = new ListOfTickets();
        public bool isEmpty()
        {
            if (HighPriority.isEmpty() && LowPriority.isEmpty())
                return true;
            else
                return false;
        }
        public void PrintAll()
        {
            HighPriority.PrintAll();
            LowPriority.PrintAll();
        }

        public void AddTicket(string s, Priority p)
        {
            Ticket a = new Ticket(s, p);
            if (p == Priority.High)
                HighPriority.AddTicket(a);
            else
                LowPriority.AddTicket(a);
        }
        public Ticket RemoveNextTicket()
        {
            if (isEmpty())
                return null;
            else
            {
                if (HighPriority.isEmpty())
                {
                    return LowPriority.RemoveNextTicket();
                }
                else
                {
                    return HighPriority.RemoveNextTicket();
                }
            }
        }

    }
}
