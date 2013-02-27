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
            <header messagekind="2" />
            <registerinfo email="name@provider.domain" password="superSavePw" nickname="Name" />
        </fowamessage>
    */

    public class RegisterMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        public RegisterMessage(string email, string password, string nickName)
            : base((int)MessageKind.RegisterMessage)
        {
            // registerinfo
            XmlNode registerInfoNode = XmlDoc.CreateElement("registerinfo");

            // email attribute
            XmlAttribute emailAttribute = XmlDoc.CreateAttribute("email");
            emailAttribute.Value = email;

            // password attribute
            XmlAttribute passwordAttribute = XmlDoc.CreateAttribute("password");
            passwordAttribute.Value = password;

            // nickName attribute
            XmlAttribute nickNameAttribute = XmlDoc.CreateAttribute("nickname");
            nickNameAttribute.Value = nickName;

            // add attribtues to retisterinfo
            if (registerInfoNode.Attributes != null)
            {
                registerInfoNode.Attributes.Append(emailAttribute);
                registerInfoNode.Attributes.Append(passwordAttribute);
                registerInfoNode.Attributes.Append(nickNameAttribute);
            }
            else
            {
                throw new Exception("Error occurred during creating a RegisterMessage");
            }

            // add researchcriteria to xml RoodNode
            RoodNode.AppendChild(registerInfoNode);

            Message = XmlDocToString(XmlDoc);
        }
    }
}
