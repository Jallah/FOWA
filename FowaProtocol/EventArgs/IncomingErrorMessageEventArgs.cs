using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.FowaImplementations;

namespace FowaProtocol.EventArgs
{
    public class IncomingErrorMessageEventArgs : System.EventArgs
    {
        public int Errorcode { get; private set; }
        public string Message { get; private set; }
        public FowaClient FowaClient { get; set; }

        public IncomingErrorMessageEventArgs(int errorCode, string message, FowaClient fowaClient)
        {
            this.Errorcode = errorCode;
            this.Message = message;
            this.FowaClient = fowaClient;
        }
    }
}
