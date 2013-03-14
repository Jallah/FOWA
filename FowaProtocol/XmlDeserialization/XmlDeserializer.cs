using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FowaProtocol.XmlDeserialization
{
    public static class XmlDeserializer
    {
        static public List<Friend> DeserializeFriends(string xmlFriendList)
        {
            XDocument doc = XDocument.Parse(xmlFriendList);

            var friends = (from f in doc.Descendants("friend")
                              let email = f.Attribute("email")
                              where email != null
                              let nickname = f.Attribute("nickname")
                              where nickname != null
                              let uid = f.Attribute("uid")
                              where uid != null

                              select new Friend
                              {
                                  Email = email.Value,
                                  Nick = nickname.Value,
                                  Uid = int.Parse(uid.Value)
                              }).ToList<Friend>();
            return friends;
        } 
    }
}
