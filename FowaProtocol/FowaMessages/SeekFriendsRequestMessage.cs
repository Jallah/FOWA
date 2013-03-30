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
            <header messagekind="4" />
            <researchcriteria email="hans@penis.de" nickname="hans" id="345" />
        </fowamessage> 
    */

    public class SeekFriendsRequestMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        public SeekFriendsRequestMessage(string email, string nickName, int id)
            : base((int)MessageKind.SeekFriendsRequestMessage)
        {
            // research criteria
            XmlNode researchCriteriaNode = XmlDoc.CreateElement("researchcriteria");

            // email attribute
            XmlAttribute emailAttribute = XmlDoc.CreateAttribute("email");
            emailAttribute.Value = email;

            // nickName attribute
            XmlAttribute nickNameAttribute = XmlDoc.CreateAttribute("nickname");
            nickNameAttribute.Value = nickName;

            // id attribute
            XmlAttribute idAttribute = XmlDoc.CreateAttribute("id");
            idAttribute.Value = id + "";

            // add attribtues to researchcriteria
            if (researchCriteriaNode.Attributes != null)
            {
                researchCriteriaNode.Attributes.Append(emailAttribute);
                researchCriteriaNode.Attributes.Append(nickNameAttribute);
                researchCriteriaNode.Attributes.Append(idAttribute);
            }
            else
            {
                throw new Exception("Error occurred during creating a SeekFriendsRequestMessage");
            }

            // add researchcriteria to xml RoodNode
            RoodNode.AppendChild(researchCriteriaNode);

            Message = XmlDocToString(XmlDoc);
        }
    }
}
