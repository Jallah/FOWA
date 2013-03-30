using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FowaProtocol.FowaModels;
using FowaProtocol.MessageEnums;

namespace FowaProtocol.FowaMessages
{
    // The Following Code will create the XML-Document shown below.

    /*
        <fowamessage>
            <header messagekind="3" />
            <sender uid="234" email="asdf" nick="asdf" />
            <receiver uid="222" email="asdf" nick="asdf" />
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
            XmlAttribute senderEmailAttribute = XmlDoc.CreateAttribute("email");
            senderEmailAttribute.Value = sender.Email;
            XmlAttribute senderNickNameAttribute = XmlDoc.CreateAttribute("nickname");
            senderNickNameAttribute.Value = sender.Nick;
            if (senderNode.Attributes != null)
            {
                senderNode.Attributes.Append(senderUidAttribute);
                senderNode.Attributes.Append(senderEmailAttribute);
                senderNode.Attributes.Append(senderNickNameAttribute);
            }
            else
                throw new Exception("Error occurred during creating a UserMessage");

            // receiver
            XmlNode receiverNode = XmlDoc.CreateElement("receiver");

            // receiver/uid
            XmlAttribute receiverUidAttribute = XmlDoc.CreateAttribute("uid");
            receiverUidAttribute.Value = receiver.UserId + "";
            XmlAttribute receiverEmailAttribute = XmlDoc.CreateAttribute("email");
            receiverEmailAttribute.Value = receiver.Email;
            XmlAttribute receiverNickNameAttribute = XmlDoc.CreateAttribute("nickname");
            receiverNickNameAttribute.Value = receiver.Nick;
            if (receiverNode.Attributes != null)
            {
                receiverNode.Attributes.Append(receiverUidAttribute);
                receiverNode.Attributes.Append(receiverEmailAttribute);
                receiverNode.Attributes.Append(receiverNickNameAttribute);
            }
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
