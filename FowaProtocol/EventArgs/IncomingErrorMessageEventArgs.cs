using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.EventArgs
{
    public class IncomingErrorMessageEventArgs : System.EventArgs
    {
        public int Errorcode { get; private set; }
        public string Message { get; private set; }
        public NetworkStream SenderNetworkStream { get; private set; }

        public IncomingErrorMessageEventArgs(int errorCode,string message, NetworkStream senderNetworkStream)
        {
            this.Errorcode = errorCode;
            this.Message = message;
            this.SenderNetworkStream = senderNetworkStream;
        }
    }
}
