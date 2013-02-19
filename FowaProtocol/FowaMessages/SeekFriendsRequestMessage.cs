using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaMessages
{
    public class SeekFriendsRequestMessage : IFowaMessage
    {
        public string Message { get; set; }

        public SeekFriendsRequestMessage(string email, string nickName, int id)
        {
            this.Message = @"<?xml version='1.0'?>
                               <fowamessage>
                                  <header>
                                    <info messagekind='" + (int)MessageKind.SeekFriendsRequestMessage + "' email='" + email + "' nickname='" + nickName + "' id='" + id + "'/>" +
                                 "</header>" +
                            "</fowamessage>";

        }
    }
}
