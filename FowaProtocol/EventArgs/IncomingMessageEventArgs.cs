

namespace FowaProtocol.EventArgs
{
    public class IncomingMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }

        public IncomingMessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
