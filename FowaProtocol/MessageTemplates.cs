using System.Collections.Generic;
using System.Text;
using Client;

namespace FowaProtocol
{
    public static class MessageTemplates
    {
        public static string LoginMessage(int messageKind, string email, string password)
        {
            var loginMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + messageKind + "' email='" + email + "' password='" + password + "'/>" +
                                   "</header>" +
                                "</fowamessage>";

            return loginMessage;
        }

        public static string RegisterMessage(int messageKind, string email, string password, string nickName)
        {
            var registerMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + messageKind + "' email='" + email + "' password='" + password + " nickname='" + nickName+ "'/>" +
                                   "</header>" +
                                "</fowamessage>";

            return registerMessage;
        }

        public static string UserMessage(int messageKind, string sender, string text)
        {
            var userMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + messageKind + "' sender='" + sender + "'/>" +
                                   "</header>" +
                                   "<message>" + text + "</message>" +
                                "</fowamessage>";

            return userMessage;
        }

        public static string SeekFriendsMessage(int messageKind, string email, string nickName, int id)
        {
            var seekFriendsMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + messageKind + "' email='" + email + "' nickname='" + nickName + "' id='" + id + "'/>" +
                                   "</header>" +
                                "</fowamessage>";

            return seekFriendsMessage;
        }

        public static string ErrorMessage(int messageKind, string message)
        {
            var errorMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='"+ messageKind +"'/>" +
                                   "</header>" +
                                   "<message>" + message + "</message>" +
                                "</fowamessage>";

            return errorMessage;
        }

        public static string FriendListMessage(int messageKind, IEnumerable<Contact> friends)
        {
            StringBuilder friendList = new StringBuilder();

            friendList.Append("<friendlist>");
            foreach (var contact in friends)
            {
                friendList.Append("<friend nickname='" + contact.NickName + "' id='" + contact.UserId + "'></friend>");
            }
            friendList.Append("</friendlist>");

            var friendListMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + messageKind + "'/>" +
                                    "</header>" +
                                    friendList +
                                "</fowamessage>";

            return friendListMessage;
        }
    }
}
