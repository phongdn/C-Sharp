using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk
{
    class ListOfTickets
    {
        public Ticket Back;
        //public Ticket Temp;
        //Temp should be a local variable because if any methods do any change to temp, it stays that way and could cause problems for other methods using the variable.
        //In a local instance of the variable, once visual studios leaves the method, that instance is no longer used and temp is reset at every instance the method is called.
        public Ticket Front;
        public bool isEmpty()
        {
            if (Front == null)
                return true;
            else
                return false;
        }
        public void PrintAll()
        {
            Ticket Temp;
            Temp = Front;
            while(Temp != null)
            {
                Temp.Print();
                Temp = Temp.Next;
            } 
        }
        public void AddTicket(Ticket t)
        {
            if(isEmpty())
            {
                Front = t;
                Back = Front;
            }
            else
            {
                Back.Next = t;
                Back = Back.Next;
            }
        }

        public Ticket RemoveNextTicket()
        {
            Ticket Temp;
            if (isEmpty())
                return null;
            else
            {
                Temp = Front;
                Front = Front.Next;
                return Temp;
            }
        }
    }
}
