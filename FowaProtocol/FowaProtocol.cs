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

/*
example 1:
   (IncomingLoginMessage != null) /1
                                  /2
    IncomingLoginMessage(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream))); /3
 
 better:
  
 example 2:
 
 var ilm = IncomingLoginMessage;
 if (ilm != null) /1
                  /2
    ilm(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream))); /3
 
 For multithreading issues it is important to crete a reference to the event.
 Assume the following: (concering to example 1)
 
 * 
 1: Thread A goes through "/1" and Incoming
 2: Thrad A is preempted by Thread B
 3: Thred in Thread B the event is set to null (for example: unsubscribe all eventhandler ---> event is equals null)
 4: Thrad B is preempted by Thread A. Thread A continues working at step "/2".
    Now IincomingLoginMessage ins null and the execution of step "/3" will cause a NullReferenceExcepiton.
 * 
 
 Example 2 is an solution. If the event will by null at step "/2" it doesen't matter and the event will still be executed.
     
*/


namespace FowaProtocol
{
    public abstract class FowaProtocol
    {
        protected FowaProtocol()
        {}

        public event EventHandler<IncomingMessageEventArgs>  IncomingLoginMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingRegisterMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingUserMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingSeekFriendsRequestMessage;
        public event EventHandler<IncomingErrorMessageEventArgs> IncomingErrorMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingFriendlistMessage;

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
                    var ilm = IncomingLoginMessage;
                    if (ilm != null)
                        ilm(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.RegisterMessage:
                    var irm = IncomingRegisterMessage;
                    if (irm != null)
                        irm(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.UserMessage:
                    var ium = IncomingUserMessage;
                    if (ium != null)
                        ium(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.SeekFriendsRequestMessage:
                    var isfm = IncomingSeekFriendsRequestMessage;
                    if (isfm != null)
                        isfm(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                case (int)MessageKind.ErrorMessage:
                    var iem = IncomingErrorMessage;
                    if (iem != null)

                        switch (GetErrorCode(message))
                        {
                            case (int)ErrorMessageKind.LiginError:
                                iem(this, new IncomingErrorMessageEventArgs((int)ErrorMessageKind.LiginError, message, new FowaClient(senderNetwrokStream)));
                                break;
                            case (int)ErrorMessageKind.RegisterError:
                                iem(this, new IncomingErrorMessageEventArgs((int)ErrorMessageKind.RegisterError, message, new FowaClient(senderNetwrokStream)));
                                break;
                        }

                    break;
                case (int)MessageKind.FriendListMessage:
                    var iflm = IncomingFriendlistMessage;
                    if (iflm != null)
                        iflm(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));
                    break;
                default:
                    throw new NotSupportedException("Not Supported");
            }
        }

    }
}
