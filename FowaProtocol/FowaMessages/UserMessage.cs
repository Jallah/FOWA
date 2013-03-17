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
            <sender uid="234" />
            <receiver uid="222" />
            <message>some text</message>
        </fowamessage> 
    */

    public class UserMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        public UserMessage(IContact sender, IContact receiver, string message):base((int)MessageKind.UserMessage)
        {
            //sender
            XmlNode senderNode = XmlDoc.CreateElement("sender");

            //sender/uid
            XmlAttribute senderUidAttribute = XmlDoc.CreateAttribute("uid");
            senderUidAttribute.Value = sender.UserId + "";
            if (senderNode.Attributes != null)
                senderNode.Attributes.Append(senderUidAttribute);
            else
                throw new Exception("Error occurred during creating a UserMessage");

            // receiver
            XmlNode receiverNode = XmlDoc.CreateElement("receiver");

            // receiver/uid
            XmlAttribute receiverUidAttribute = XmlDoc.CreateAttribute("uid");
            receiverUidAttribute.Value = receiver.UserId + "";
            if (receiverNode.Attributes != null)
                receiverNode.Attributes.Append(receiverUidAttribute);
            else
                throw new Exception("Error occurred during creating a UserMessage");

            // messsagenode
            XmlNode messageNode = XmlDoc.CreateElement("message");
            messageNode.InnerText = message;

            // add senderNode to RoodNode
            RoodNode.AppendChild(senderNode);

            // add receiverNode to RoodNode
            RoodNode.AppendChild(receiverNode);

            //add essagenode to RoodNode
            RoodNode.AppendChild(messageNode);

            Message = XmlDocToString(XmlDoc);
        }
    }
}
