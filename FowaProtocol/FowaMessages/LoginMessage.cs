using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaMessages
{
    public class LoginMessage : IFowaMessage
    {
        // implement IFowaMessage
        public string Message { get; set; } // hier versuchen setter private zu machen 

        public LoginMessage(string email, string password)
        {
            this.Message = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + (int)MessageKind.LoginMessage + "' email='" + email + "' password='" + password + "'/>" +
                                   "</header>" +
                                "</fowamessage>"; 
        }
       
    }
}
