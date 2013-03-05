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

        public Action<object, IncomingMessageEventArgs> OnIncomingLoginMessageCallback { get; set; }
        public Action<object, IncomingMessageEventArgs> OnIncomingRegisterMessageeCallback { get; set; }
        public Action<object, IncomingMessageEventArgs> OnIncomingUserMessageCallback { get; set; }
        public Action<object, IncomingMessageEventArgs> OnIncomingSeekFriendsRequestMessageCallback { get; set; }
        public Action<object, IncomingErrorMessageEventArgs> OnIncomingErrorMessageCallback { get; set; }
        public Action<object, IncomingMessageEventArgs> OnIncomingFriendlistMessageCallback { get; set; }

    }
}
