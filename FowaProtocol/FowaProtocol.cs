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
 2: Thread A is preempted by Thread B
 3: Thread B sets the event to null (for example: unsubscribe all eventhandler ---> event is equals null)
 4: Thrad B is preempted by Thread A. Thread A continues working at step "/2".
    Now IincomingLoginMessage is null and the execution of step "/3" will cause a NullReferenceExcepiton.
 * 
 
 Example 2 is an solution. If the event will by null at step "/2" it doesen't matter and the event will still be executed.
     
*/


namespace FowaProtocol
{
    public abstract class FowaProtocol
    {
        protected FowaProtocol()
        { }

        public event EventHandler<IncomingMessageEventArgs> IncomingLoginMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingRegisterMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingUserMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingSeekFriendsRequestMessage;
        public event EventHandler<IncomingErrorMessageEventArgs> IncomingErrorMessage;
        public event EventHandler<IncomingMessageEventArgs> IncomingFriendlistMessage;

        // if attribute = header
        // LoginMessage = 1
        // RegisterMessage = 2
        // UserMessage = 3
        // SeekFriendsRequestMessage = 4
        // ErrorMessage = 5
        // FriendlistMessage = 6
        //
        // if attribute = errorinfo
        // LoginError = 1
        // RegisterError = 2
        protected int GetKindOfMessageOrErrorCode(string incomingString, string attribute) // maybe solve this with an enum instead of an string for the attrubute parameter
        {
            int messageKindOrErrorCode;
            using (XmlReader reader = XmlReader.Create(new StringReader(incomingString)))
            {
                reader.ReadToFollowing(attribute);
                reader.MoveToFirstAttribute();
                messageKindOrErrorCode = int.Parse(reader.Value);
            }

            return messageKindOrErrorCode;
        }


        public virtual MessageKind HandleIncomingMessage(string message, NetworkStream senderNetwrokStream)
        {
            EventHandler<IncomingMessageEventArgs> incomingMessageEvent = null;
            MessageKind messageKind = (MessageKind)GetKindOfMessageOrErrorCode(message, "header");

            switch (messageKind)
            {
                case MessageKind.LoginMessage:
                    incomingMessageEvent = IncomingLoginMessage;
                    break;
                case MessageKind.RegisterMessage:
                    incomingMessageEvent = IncomingRegisterMessage;
                    break;
                case MessageKind.UserMessage:
                    incomingMessageEvent = IncomingUserMessage;
                    break;
                case MessageKind.SeekFriendsRequestMessage:
                    incomingMessageEvent = IncomingSeekFriendsRequestMessage;
                    break;
                case MessageKind.ErrorMessage:
                    var iem = IncomingErrorMessage;
                    ErrorMessageKind errorMessageKind = (ErrorMessageKind)GetKindOfMessageOrErrorCode(message, "errorinfo");

                    switch (errorMessageKind)
                    {
                        case ErrorMessageKind.LiginError:
                            if (iem != null)
                                iem(this, new IncomingErrorMessageEventArgs((int)ErrorMessageKind.LiginError, message, new FowaClient(senderNetwrokStream)));
                            break;
                        case ErrorMessageKind.RegisterError:
                            if (iem != null)
                                iem(this, new IncomingErrorMessageEventArgs((int)ErrorMessageKind.RegisterError, message, new FowaClient(senderNetwrokStream)));
                            break;
                    }

                    break;
                case MessageKind.FriendListMessage:
                    incomingMessageEvent = IncomingFriendlistMessage;
                    break;
                default:
                    messageKind = MessageKind.UnknownMessage;
                    break;
            }

            if (incomingMessageEvent != null)
                incomingMessageEvent(this, new IncomingMessageEventArgs(message, new FowaClient(senderNetwrokStream)));

            return messageKind;
        }

    }
}
