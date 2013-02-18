using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol
{
    public abstract class FowaProtocol
    {
        protected FowaProtocol()
        {
            this.IncomingLoginMessage += OnIncomingLoginMessage;
            this.IncomingRegisterMessage += OnIncomingRegisterMessage;
            this.IncomingUserMessage += OnIncomingUserMessage;
            this.IncomingSeekFriendsRequestMessage += OnIncomingSeekFriendsRequestMessage;
            this.IncomingErrorMessage += OnIncomingErrorMessage;
            this.IncomingFriendlistMessage += OnIncomingFriendlistMessage;
        }


        public delegate void IncomingLoginMessageEventHandler(object sender, IncomingMessageEventArgs args);
        public delegate void IncomingRegisterMessageEventHandler(object sender, IncomingMessageEventArgs args);
        public delegate void IncomingUserMessageEventHandler(object sender, IncomingMessageEventArgs args);
        public delegate void IncomingSeekFriendsRequestMessageEventHandler(object sender, IncomingMessageEventArgs args);
        public delegate void IncomingErrorMessageMessageEventHandler(object sender, IncomingMessageEventArgs args);
        public delegate void IncomingFriendlistMessageEventHandler(object sender, IncomingMessageEventArgs args);

        public event IncomingLoginMessageEventHandler IncomingLoginMessage;
        public event IncomingRegisterMessageEventHandler IncomingRegisterMessage;
        public event IncomingUserMessageEventHandler IncomingUserMessage;
        public event IncomingSeekFriendsRequestMessageEventHandler IncomingSeekFriendsRequestMessage;
        public event IncomingErrorMessageMessageEventHandler IncomingErrorMessage;
        public event IncomingFriendlistMessageEventHandler IncomingFriendlistMessage;

        protected abstract void OnIncomingLoginMessage(object sender, IncomingMessageEventArgs args);
        protected abstract void OnIncomingRegisterMessage(object sender, IncomingMessageEventArgs args);
        protected abstract void OnIncomingUserMessage(object sender, IncomingMessageEventArgs args);
        protected abstract void OnIncomingSeekFriendsRequestMessage(object sender, IncomingMessageEventArgs args);
        protected abstract void OnIncomingErrorMessage(object sender, IncomingMessageEventArgs args);
        protected abstract void OnIncomingFriendlistMessage(object sender, IncomingMessageEventArgs args);

        // LoginMessage = 1,
        // RegisterMessage = 2
        // UserMessage = 3
        // SeekFriendsRequestMessage = 4
        // ErrorMessage = 5
        // FriendlistMessage = 6
        protected abstract int GetKindOfMessage(string messag);
        
        protected void HandleIncomingMessage(string message)
        {
            switch (GetKindOfMessage(message))
            {
                case (int)MessageKind.LoginMessage:
                    IncomingLoginMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.RegisterMessage:
                    IncomingRegisterMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.UserMessage:
                    IncomingUserMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.SeekFriendsRequestMessage:
                    IncomingSeekFriendsRequestMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.ErrorMessage:
                    IncomingErrorMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.FriendlistMessage:
                    IncomingFriendlistMessage(this, new IncomingMessageEventArgs(message));
                    break;
                default:
                    throw new NotSupportedException("Not Supported");
            }
        }

    }
}
