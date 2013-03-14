using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol;
using System.Xml;

namespace FowaProtocol.FowaMessages
{
    // The Following Code will create the XML-Document shown below.

    /*
        <fowamessage>
           <header messagekind="6" />
           <friendlist>
                <friend email="" nickname="" uid="" />
                <friend email="" nickname="" uid="" />
                ...
          </friendlist>
        </fowamessage> 
    */

    public class FriendListMessage : MessageBase, IFowaMessage
    {
        public string Message { get; private set; }

        // not finished yet
        public FriendListMessage(IEnumerable<Friend> friends)
            : base((int)MessageKind.FriendListMessage)
        {
            // friendlistnode
            XmlNode friendlistNode = XmlDoc.CreateElement("friendlist");

            // maybe accomplish the following through Serializer ( I think it is a matter of taste )
            foreach (var friend in friends)
            {
                XmlNode friendNode = XmlDoc.CreateElement("friend");

                // Create attributes
                XmlAttribute eMailAttribute = XmlDoc.CreateAttribute("email");
                eMailAttribute.Value = friend.Email;
                XmlAttribute nickNameAttribute = XmlDoc.CreateAttribute("nickname");
                nickNameAttribute.Value = friend.Nick;
                XmlAttribute uidAttribute = XmlDoc.CreateAttribute("uid");
                uidAttribute.Value = friend.Uid + "";

                // add attributes to friendnode
                if (friendlistNode.Attributes != null)
                {
                    friendNode.Attributes.Append(eMailAttribute);
                    friendNode.Attributes.Append(nickNameAttribute);
                    friendNode.Attributes.Append(uidAttribute);
                }
                else
                {
                    throw new Exception("Error occurred during creating a FriendListMessage");
                }

                // add friendnode to freindlistnode
                friendlistNode.AppendChild(friendNode);
            }

            // add logininfo to xml RoodNode
            RoodNode.AppendChild(friendlistNode);

            Message = XmlDocToString(XmlDoc);
        }
    }
}
