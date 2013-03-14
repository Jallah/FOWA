using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FowaProtocol;
using FowaProtocol.FowaMessages;
using FowaProtocol.XmlDeserialization;
using Server;
using Server.BL.Services;
using Server.DL;

namespace Tests
{
    class Program
    {

        static void Main(string[] args)
        {
            var s = Convert.ToString(DateTime.Now);
            var d = new DateTime();
            d = DateTime.Parse(s);

            List<Friend> l = new List<Friend>
                               {
                                   new Friend(){Email = "hans", Nick = "hans", Uid = 23},
                                   new Friend(){Email = "hans", Nick = "hans", Uid = 23}
                               };

            FriendListMessage m = new FriendListMessage(l);
            

            Console.ReadKey();
        }
    }
}
