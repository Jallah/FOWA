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

        public static RegisterInfo GetRegisterInfo(string xmlMessage)
        {
            XDocument doc = XDocument.Parse(xmlMessage);

            var registerInfo = (from i in doc.Descendants("registerinfo")
                             let email = i.Attribute("email")
                             where email != null
                             let password = i.Attribute("password")
                             where password != null
                             let nickname = i.Attribute("nickname")
                             where nickname != null

                             select new RegisterInfo()
                             {
                                 Email = email.Value,
                                 Pw = password.Value,
                                 NickName = nickname.Value
                                 
                             }).FirstOrDefault();

            return registerInfo;
        }

        public static string GetMessage(string xmlErrorMessage)
        {
            XDocument doc = XDocument.Parse(xmlErrorMessage);
            XElement messageElement = doc.Descendants("message").FirstOrDefault();

            return messageElement != null ? messageElement.Value : null;
        }

        public static Friend GetLoggedInAsInfo(string xmlMessage)
        {
            XDocument doc = XDocument.Parse(xmlMessage);

            var logedinas = (from i in doc.Descendants("loggedinas")
                             let email = i.Attribute("email")
                             where email != null
                             let nickname = i.Attribute("nickname")
                             where nickname != null
                             let uid = i.Attribute("uid")
                             where uid != null

                             select new Friend()
                                        {
                                            Email = email.Value,
                                            Nick = nickname.Value,
                                            UserId = int.Parse(uid.Value)
                                        }).FirstOrDefault();

            return logedinas;
        }

        public static IContact GetUserFromUserMessage(string xmlMessage, UserMessageElement element)
        {
            XDocument doc = XDocument.Parse(xmlMessage);

            string messgeElement = element.ToString().ToLower();

            var user = (from i in doc.Descendants(messgeElement)
                               let uid = i.Attribute("uid")
                               where uid != null
                               let email = i.Attribute("email")
                               where email != null
                               let nickname = i.Attribute("nickname")
                               where nickname != null

                               select new Friend()
                                          {
                                              UserId = int.Parse(uid.Value),
                                              Email = email.Value,
                                              Nick = nickname.Value
                                          }).FirstOrDefault();

            return user;
        }
    }
}
