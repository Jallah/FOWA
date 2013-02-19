using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaMessages
{
    public class ErrorMessage : IFowaMessage
    {
        public string Message { get; set; }


        public ErrorMessage(string message)
        {
            this.Message = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + (int)MessageKind.ErrorMessage + "'/>" +
                                   "</header>" +
                                   "<message>" + message + "</message>" +
                                "</fowamessage>";
        }
    }
}
