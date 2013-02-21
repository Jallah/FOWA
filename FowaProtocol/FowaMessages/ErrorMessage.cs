using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.MessageEnums;

namespace FowaProtocol.FowaMessages
{
    public class ErrorMessage : IFowaMessage
    {
        public string Message { get; set; }

        public ErrorMessage(ErrorMessageKind errorMessage, string message)
        {
            this.Message = @"<?xml version='1.0'?>
                                 <fowamessage>
                                    <header>
                                        <info messagekind='" + (int)MessageKind.ErrorMessage + "' errorcode='"+ (int)errorMessage + "'/>" +
                                   "</header>" +
                                   "<message>" + message + "</message>" +
                                "</fowamessage>";
        }
    }
}
