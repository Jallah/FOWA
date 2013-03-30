using System;
using System.Net.Sockets;
using FowaProtocol.FowaMessages;
using FowaProtocol.FowaModels;
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
            if(OnFoo != null)
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
        private static void Demo()
        {

            Action[] func = new Action[10];
            for (int i = 0; i < 10; i++)
            {
                int j = i;

                func[i] = (delegate()
                {
                    Console.WriteLine("Wert von i: {0}", j);
                    // anstatt i hier j verwenden
                });
            }

            foreach (Action item in func)
                item();
        }  

        static void Main(string[] args)
        {
            //Sender s = new Sender();
            //Subscriber su = new Subscriber();

            //s.OnFoo -= su.OnFooEventHandler;

            //s.RaiseEvent();

            //su = null;

            ////s.RaiseEvent(); // hier hab ich erwatet das das der EventHanlder nich mehr gültig ist und somit nicht mehr ausgeführt wird
            //Demo();
            UserMessage userMessage = new UserMessage(new Friend { Email = "abc", Nick = "abc", UserId = 123 }, new Friend { Email = "abc", Nick = "abc", UserId = 123 }, "hall");
            Console.WriteLine(userMessage.Message);
            var sender = XmlDeserializer.GetUserFromUserMessage(userMessage.Message, UserMessageElement.Sender);

            Console.WriteLine(XmlDeserializer.GetMessage(userMessage.Message));
            Console.ReadKey(); 
        }
    }
}
