

using System.Net.Sockets;

namespace FowaProtocol.EventArgs
{
    public class IncomingMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }
        public NetworkStream SenderNetworkStream { get; private set; }

        public IncomingMessageEventArgs(string message, NetworkStream senderNetworkStream)
        {
            this.Message = message;
            this.SenderNetworkStream = senderNetworkStream;
        }
    }
}
