using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FowaProtocol.FowaMessages
{
    // This Base-Class will create the roodNode, headerNode, and one messageKind-attribute for each message.

    /*
     <fowamessage>
        <header messagekind="" />
        ...
     </fowamessage> 
    */

    public class MessageBase
    {
        protected XmlDocument XmlDoc { get; private set; }
        protected XmlNode RoodNode { get; private set; }

        public MessageBase(int messageKind)
        {
            XmlDoc = new XmlDocument();
            XmlDoc.CreateXmlDeclaration("1.0", null, null);

            // rootNode
            RoodNode = XmlDoc.CreateElement("fowamessage");
            XmlDoc.AppendChild(RoodNode);

            //header
            XmlNode headerNode = XmlDoc.CreateElement("header");

            //header/messagekind
            XmlAttribute messageKindAttribute = XmlDoc.CreateAttribute("messagekind");
            messageKindAttribute.Value = messageKind + "";
            if (headerNode.Attributes != null) 
                headerNode.Attributes.Append(messageKindAttribute);
            else
                throw new Exception("Error occurred during creating a Message");
           
            // add header,sender and receiver to RoodNode
            RoodNode.AppendChild(headerNode);

        }

        protected string XmlDocToString(XmlDocument xmlDoc)
        {
            //using (StringWriter sw = new StringWriter())
            //using (XmlTextWriter xmlTextWriter = new XmlTextWriter(sw))
            //{
            //    xmlDoc.WriteTo(xmlTextWriter);

            //    return sw.ToString();
            //}


            // or just
            return xmlDoc.OuterXml;
        }

    }
}
