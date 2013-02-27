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
            <header messagekind="1" />
            <logininfo email="name@provider.domain" password="superSavePw" />
        </fowamessage>
    */

    public class LoginMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        public LoginMessage(string email, string password)
            : base((int)MessageKind.LoginMessage)
        {
            // logininfo
            XmlNode loginInfoNode = XmlDoc.CreateElement("logininfo");

            // email attribute
            XmlAttribute emailAttribute = XmlDoc.CreateAttribute("email");
            emailAttribute.Value = email;

            // password attribute
            XmlAttribute passwordAttribute = XmlDoc.CreateAttribute("password");
            passwordAttribute.Value = password;

            // add attribtues to logininfo
            if (loginInfoNode.Attributes != null)
            {
                loginInfoNode.Attributes.Append(emailAttribute);
                loginInfoNode.Attributes.Append(passwordAttribute);
            }
            else
            {
                throw new Exception("Error occurred during creating a LoginMessage");
            }

            // add logininfo to xml RoodNode
            RoodNode.AppendChild(loginInfoNode);

            Message = XmlDocToString(XmlDoc);

        }

    }
}
