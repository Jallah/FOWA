﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol;

namespace FowaProtocol.FowaMessages
{
    public class FriendListMessage : IFowaMessage
    {
        public string Message { get; set; }

        public FriendListMessage(IEnumerable<Contact> friends)
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
                                        <info messagekind='" + (int)MessageKind.FriendListMessage + "'/>" +
                                    "</header>" +
                                    friendList +
                                "</fowamessage>";

            this.Message = friendListMessage;
        }
    }
}