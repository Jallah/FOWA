using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol
{
    public class IncomingMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public IncomingMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
