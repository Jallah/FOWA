﻿using System.IO;
using System.Xml;
using FowaProtocol.FowaMessages;
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
            //this.IncomingLoginMessage += OnIncomingLoginMessage;
            //this.IncomingRegisterMessage += OnIncomingRegisterMessage;
            //this.IncomingUserMessage += OnIncomingUserMessage;
            //this.IncomingSeekFriendsRequestMessage += OnIncomingSeekFriendsRequestMessage;
            //this.IncomingErrorMessage += OnIncomingErrorMessage;
            //this.IncomingFriendlistMessage += OnIncomingFriendlistMessage;
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

        

        // LoginMessage = 1,
        // RegisterMessage = 2
        // UserMessage = 3
        // SeekFriendsRequestMessage = 4
        // ErrorMessage = 5
        // FriendlistMessage = 6
        protected int GetKindOfMessage(string incomingString)
        {
            int messageKind;
            using(XmlReader reader = XmlReader.Create(new StringReader(incomingString)))
            {
                reader.ReadToFollowing("info");
                reader.MoveToFirstAttribute();
                messageKind = int.Parse(reader.Value);
            }

            return messageKind;
        }
        
        protected void HandleIncomingMessage(string message)
        {

            switch (GetKindOfMessage(message))
            {
                case (int)MessageKind.LoginMessage:
                    if(IncomingLoginMessage != null)
                    IncomingLoginMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.RegisterMessage:
                    if(IncomingRegisterMessage != null)
                    IncomingRegisterMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.UserMessage:
                    if(IncomingUserMessage != null)
                    IncomingUserMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.SeekFriendsRequestMessage:
                    if(IncomingSeekFriendsRequestMessage != null)
                    IncomingSeekFriendsRequestMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.ErrorMessage:
                    if(IncomingErrorMessage != null)
                    IncomingErrorMessage(this, new IncomingMessageEventArgs(message));
                    break;
                case (int)MessageKind.FriendListMessage:
                    if(IncomingFriendlistMessage != null)
                    IncomingFriendlistMessage(this, new IncomingMessageEventArgs(message));
                    break;
                default:
                    throw new NotSupportedException("Not Supported");
            }
        }

    }
}
