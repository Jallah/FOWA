using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.EventArgs;

namespace FowaProtocol
{
    public class FowaMetaData
    {

        public EventHandler<IncomingMessageEventArgs> OnIncomingLoginMessageCallback { get; set; }
        public EventHandler<IncomingMessageEventArgs> OnIncomingRegisterMessageeCallback { get; set; }
        public EventHandler<IncomingMessageEventArgs> OnIncomingUserMessageCallback { get; set; }
        public EventHandler<IncomingMessageEventArgs> OnIncomingSeekFriendsRequestMessageCallback { get; set; }
        public EventHandler<IncomingErrorMessageEventArgs> OnIncomingErrorMessageCallback { get; set; }
        public EventHandler<IncomingMessageEventArgs> OnIncomingFriendlistMessageCallback { get; set; }

    }
}
