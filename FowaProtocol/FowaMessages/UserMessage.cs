using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FowaProtocol.FowaMessages
{
    // The Following Code will create the XML-Document shown below.

    /*
        <fowamessage>
            <header messagekind="3" />
            <senderinfo email="" nickname="hans" ip="" uid="234" />
            <message>some text</message>
        </fowamessage> 
    */

    public class UserMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        public UserMessage(IContact sender, string message):base((int)MessageKind.UserMessage)
        {
            // senderinfonode
            XmlNode senderInfoNode = XmlDoc.CreateElement("senderinfo");

            // Create attributes
            XmlAttribute eMailAttribute = XmlDoc.CreateAttribute("email");
            eMailAttribute.Value = sender.Email;
            XmlAttribute nickNameAttribute = XmlDoc.CreateAttribute("nickname");
            nickNameAttribute.Value = sender.Nick;
            XmlAttribute ipAttribute = XmlDoc.CreateAttribute("ip");
            ipAttribute.Value = sender.Ip;
            XmlAttribute uidAttribute = XmlDoc.CreateAttribute("uid");
            uidAttribute.Value = sender.UID + "";

            // add attributes to senderinfonode
            if (senderInfoNode.Attributes != null)
            {
                senderInfoNode.Attributes.Append(eMailAttribute);
                senderInfoNode.Attributes.Append(nickNameAttribute);
                senderInfoNode.Attributes.Append(ipAttribute);
                senderInfoNode.Attributes.Append(uidAttribute);
            }
            else
            {
                throw new Exception("Error occurred during creating a UserMessage");
            }

            // messsagenode
            XmlNode messageNode = XmlDoc.CreateElement("message");
            messageNode.InnerText = message;

            // add senderinfonode to RoodNode
            RoodNode.AppendChild(senderInfoNode);

            //add messagenode to RoodNode
            RoodNode.AppendChild(messageNode);

            Message = XmlDocToString(XmlDoc);
        }
    }
}
