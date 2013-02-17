using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;

namespace FowaProtocol
{
    class Test
    {
        static void Main(string[] args)
        {

            List<Contact> list = new List<Contact>()
                                     {
                                         new Contact(){NickName = "Hans", UserId = 2},
                                         new Contact(){NickName = "Peter", UserId = 34}
                                     };

            Console.Write(MessageTemplates.FriendListMessage((int)MessageKind.FriendlistMessage, list));

            Console.Read();

        }
    }
}
