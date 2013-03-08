using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FowaProtocol.FowaMessages;
using FowaProtocol.MessageEnums;

namespace FowaProtocol
{
    //[Serializable()]
    public class Person : IContact
    {
        [XmlAttribute("email")]
        public string Email { get; set; }
        [XmlAttribute("nick")]
        public string Nick { get; set; }
        [XmlAttribute("id")]
        public int UID { get; set; }
        [XmlAttribute("ip")]
        public string Ip { get; set; }

        public string Pw { get; set; }
    }


    class Test
    {
        public static string XmlDocToString(XmlDocument xmlDoc)
        {
            using(StringWriter sw = new StringWriter())
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(sw))
            {
                xmlDoc.WriteTo(xmlTextWriter);

                return sw.ToString();
            }
        }

        static void Main(string[] args)
        {
            RegisterMessage registerMessage = new RegisterMessage("hans@penis.de", "superSavePw", "hans");
            LoginMessage loginMessage = new LoginMessage("hans@penis.de", "superSavePw");
            SeekFriendsRequestMessage seekFriendsRequestMessage = new SeekFriendsRequestMessage("hans@penis.de", "hans", 345);

            List<Person> list = new List<Person>();

            Person p1 = new Person {Nick = "hans", UID = 234};
            Person p2 = new Person {Nick = "peter",UID = 244};

            list.Add(p1);
            list.Add(p2);

            //FriendListMessage<IContact> friendListMessage = new FriendListMessage<IContact>(list);
            
            //ErrorMessage errorMessage = new ErrorMessage(ErrorMessageKind.RegisterError, "lol fail du knecht");

            //XmlSerializer serializer = new XmlSerializer(list.GetType());
            //StringWriter stringWriter = new StringWriter();

            //serializer.Serialize(stringWriter, list);

            //Console.WriteLine(stringWriter.ToString());

            UserMessage userMessage = new UserMessage(p1, p1, "some text");

    
            Console.WriteLine(userMessage.Message);
            Console.ReadKey();
        }
    }
}
