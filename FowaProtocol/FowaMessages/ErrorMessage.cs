using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FowaProtocol.MessageEnums;

namespace FowaProtocol.FowaMessages
{
    // The Following Code will create the XML-Document shown below.

    /*
        <fowamessage>
            <header messagekind="5" />
            <errorinfo errormessagekind="2" />
            <message>Some error message</errormessage>
        </fowamessage> 
    */

    public class ErrorMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        public ErrorMessage(ErrorMessageKind errorMessageKind, string errorMessage)
            : base((int)MessageKind.ErrorMessage)
        {
            // errorinfo
            XmlNode erroInfoNode = XmlDoc.CreateElement("errorinfo");

            // errormessagekind attribute
            XmlAttribute errorMessageKindAttribute = XmlDoc.CreateAttribute("errormessagekind");
            errorMessageKindAttribute.Value = (int)errorMessageKind + "";

            // add errormessagekind attribute to erroinfonode
            if (erroInfoNode.Attributes != null)
            {
                erroInfoNode.Attributes.Append(errorMessageKindAttribute);
            }
            else
            {
                throw new Exception("Error occurred during creating ErrorMessage");
            }

            // errormessagenode
            XmlNode errorMessageNode = XmlDoc.CreateElement("message");
            errorMessageNode.InnerText = errorMessage;

            // add errorinfonode to xml RoodNode
            RoodNode.AppendChild(erroInfoNode);

            // add errormessagenode to xml RoodNode
            RoodNode.AppendChild(errorMessageNode);

            Message = XmlDocToString(XmlDoc);
        }
    }
}
