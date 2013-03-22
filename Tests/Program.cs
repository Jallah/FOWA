using System;
using FowaProtocol.FowaMessages;
using FowaProtocol.MessageEnums;
using FowaProtocol.XmlDeserialization;
using Server.BL.Services;
using Server.DL;

namespace Tests
{
    public class Sender
    {
        public event EventHandler<EventArgs> OnFoo; 

        public void RaiseEvent()
        {
            OnFoo(this, new EventArgs());
        }
    }

    public class Subscriber
    {
        public void OnFooEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("fire fire fire");
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            Sender s = new Sender();
            Subscriber su = new Subscriber();

            s.OnFoo += su.OnFooEventHandler;

            s.RaiseEvent();

            su = null;

            s.RaiseEvent(); // hier hab ich erwatet das das der EventHanlder nich mehr gültig ist und somit nicht mehr ausgeführt wird

            Console.ReadKey(); 
        }
    }
}
