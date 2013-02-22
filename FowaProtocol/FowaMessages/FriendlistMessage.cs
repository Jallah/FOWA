using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol;

namespace FowaProtocol.FowaMessages
{
    public class FriendListMessage<T> : IFowaMessage where T : IContact
    {
        public string Message { get; set; }

        // not finished yet
        public FriendListMessage(IEnumerable<T> friends)
        {
            StringBuilder friendList = new StringBuilder();

            friendList.Append("<friendlist>");
            foreach (var contact in friends)
            {
                friendList.Append("<friend nickname='" + contact.Nick + "' id='" + contact.UID + "'></friend>");
            }
            friendList.Append("</friendlist>");

            var friendListMessage = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + (int)MessageKind.FriendListMessage + "'/>" +
                                    "</header>" +
                                    friendList +
                                "</fowamessage>";

            this.Message = friendListMessage;
        }
    }
}
