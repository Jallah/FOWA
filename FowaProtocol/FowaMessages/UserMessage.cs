using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaMessages
{
    public class UserMessage : IFowaMessage
    {
        public string Message { get; set; }

        public UserMessage(string sender, string text)
        {
            this.Message = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + (int)MessageKind.UserMessage + "' sender='" + sender + "'/>" +
                                   "</header>" +
                                   "<message>" + text + "</message>" +
                                "</fowamessage>";
        }
    }
}
