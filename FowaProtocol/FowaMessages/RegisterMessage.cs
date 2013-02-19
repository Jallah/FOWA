using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaMessages
{
    public class RegisterMessage : IFowaMessage
    {
        public string Message { get; set; }

        public RegisterMessage(string email, string password, string nickName)
        {
            this.Message = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + (int)MessageKind.RegisterMessage + "' email='" + email + "' password='" + password + " nickname='" + nickName + "'/>" +
                                    "</header>" +
                                 "</fowamessage>";
        }
    }
}
