using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FowaProtocol.FowaModels;

namespace FowaProtocol.XmlDeserialization
{
    public static class XmlDeserializer
    {
        public static List<Friend> DeserializeFriends(string xmlFriendList)
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
                                  UserId = int.Parse(uid.Value)
                              }).ToList<Friend>();
            return friends;
        }

        public static LoginInfo GetLoginInfo(string xmlLoginInfo)
        {
            XDocument doc = XDocument.Parse(xmlLoginInfo);

            var loginInfo = (from i in doc.Descendants("logininfo")
                             let email = i.Attribute("email")
                             where email != null
                             let password = i.Attribute("password")
                             where password != null

                             select new LoginInfo
                                        {
                                            Email = email.Value,
                                            Pw = password.Value
                                        }).FirstOrDefault();
            return loginInfo;
        }

        public static string GetErrorMessage(string xmlErrorMessage)
        {
            XDocument doc = XDocument.Parse(xmlErrorMessage);
            XElement messageElement = doc.Descendants("errormessage").FirstOrDefault();

            return messageElement != null ? messageElement.Value : null;
        }

        
    }
}
