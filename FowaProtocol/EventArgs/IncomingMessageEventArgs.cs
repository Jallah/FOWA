

using System.Net.Sockets;
using FowaProtocol.FowaImplementations;

namespace FowaProtocol.EventArgs
{
    public class IncomingMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }
        //public NetworkStream SenderNetworkStream { get; private set; }
        public FowaClient FowaClient { get; set; }

        public IncomingMessageEventArgs(string message, FowaClient fowaClient)
        {
            this.Message = message;
            this.FowaClient = fowaClient;
            //this.SenderNetworkStream = senderNetworkStream;
        }
    }
}
