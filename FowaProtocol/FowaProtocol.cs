using System.IO;
using System.Net.Sockets;
using System.Xml;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.MessageEnums;

namespace FowaProtocol
{
    public abstract class FowaProtocol
    {
        protected FowaProtocol()
        { }

        //public delegate void IncomingLoginMessageEventHandler(object sender, IncomingMessageEventArgs args);
        //public delegate void IncomingRegisterMessageEventHandler(object sender, IncomingMessageEventArgs args);
        //public delegate void IncomingUserMessageEventHandler(object sender, IncomingMessageEventArgs args);
        //public delegate void IncomingSeekFriendsRequestMessageEventHandler(object sender, IncomingMessageEventArgs args);
        //public delegate void IncomingErrorMessageMessageEventHandler(object sender, IncomingErrorMessageEventArgs args);
        //public delegate void IncomingFriendlistMessageEventHandler(object sender, IncomingMessageEventArgs args);

        public event Action<object, IncomingMessageEventArgs> IncomingLoginMessage;
        public event Action<object, IncomingMessageEventArgs> IncomingRegisterMessage;
        public event Action<object, IncomingMessageEventArgs> IncomingUserMessage;
        public event Action<object, IncomingMessageEventArgs> IncomingSeekFriendsRequestMessage;
        public event Action<object, IncomingErrorMessageEventArgs> IncomingErrorMessage;
        public event Action<object, IncomingMessageEventArgs> IncomingFriendlistMessage;

        // LoginMessage = 1
        // RegisterMessage = 2
        // UserMessage = 3
        // SeekFriendsRequestMessage = 4
        // ErrorMessage = 5
        // FriendlistMessage = 6
        protected int GetKindOfMessage(string incomingString)
        {
            int messageKind;
            using (XmlReader reader = XmlReader.Create(new StringReader(incomingString)))
            {
                reader.ReadToFollowing("header");
                reader.MoveToFirstAttribute();
                messageKind = int.Parse(reader.Value);
            }

            return messageKind;
        }

        // LoginError = 1
        // RegisterError = 2
        protected int GetErrorCode(string message)
        {
            int errorcode;
            using (XmlReader reader = XmlReader.Create(new StringReader(message)))
            {
                reader.ReadToFollowing("errorinfo");
                reader.MoveToFirstAttribute();
                errorcode = int.Parse(reader.Value);
            }

            return errorcode;
        }

        public virtual void HandleIncomingMessage(string message, NetworkStream senderNetwrokStream)
        {

            switch (GetKindOfMessage(message))
            {
                case (int)MessageKind.LoginMessage:
                    if (IncomingLoginMessage != null)
                        IncomingLoginMessage(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.RegisterMessage:
                    if (IncomingRegisterMessage != null)
                        IncomingRegisterMessage(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.UserMessage:
                    if (IncomingUserMessage != null)
                        IncomingUserMessage(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.SeekFriendsRequestMessage:
                    if (IncomingSeekFriendsRequestMessage != null)
                        IncomingSeekFriendsRequestMessage(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.ErrorMessage:
                    if (IncomingErrorMessage != null)

                        switch (GetErrorCode(message))
                        {
                            case (int)ErrorMessageKind.LiginError:
                                IncomingErrorMessage(this, new IncomingErrorMessageEventArgs((int)ErrorMessageKind.LiginError, message, new FowaClient(senderNetwrokStream)));
                                break;
                            case (int)ErrorMessageKind.RegisterError:
                                IncomingErrorMessage(this, new IncomingErrorMessageEventArgs((int)ErrorMessageKind.RegisterError, message, new FowaClient(senderNetwrokStream)));
                                break;
                        }

                    break;
                case (int)MessageKind.FriendListMessage:
                    if (IncomingFriendlistMessage != null)
                        IncomingFriendlistMessage(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                default:
                    throw new NotSupportedException("Not Supported");
            }
        }

    }
}
