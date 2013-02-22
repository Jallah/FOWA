using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaMessages;

namespace FowaProtocol.FowaImplementations
{
    public class FowaClient : IDisposable
    {
        private TcpClient _client;
        private const int BUFFER_SIZE = 4096; // 2^12
        public NetworkStream ClientStream { get; set; }
        public delegate void IncomingUserMessageEventHandler(object sender, IncomingMessageEventArgs args);
        public event IncomingUserMessageEventHandler IncomingUserMessage;


        // = new IPEndPoint(IPAddress.Parse(/*"127.0.0.1"*/"192.168.2.108"), 80);
        public FowaClient()
            : base()
        {
            _client = new TcpClient();
        }

        public void Connect(IPEndPoint endPoint)
        {
            try
            {
                _client.Connect(endPoint);
                ClientStream = _client.GetStream();
            }
            catch (Exception ex)
            {
                throw new Exception("Connection failed: " + ex.Message);
            }

        }

        public int WriteToClientStreamAync(IFowaMessage fowaMessage, NetworkStream networkStream)
        {
            try
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(fowaMessage.Message.Trim());
                networkStream.WriteAsync(buffer, 0, buffer.Length);
                return 0;
            }
            catch (Exception)
            {
                // Log exceptions
                return -1;
            }
        }

        public int ReadFromClientSream(NetworkStream networkStream)
        {
            if (!networkStream.CanRead) return -1;

            byte[] message = new byte[BUFFER_SIZE];
            ASCIIEncoding encoder = new ASCIIEncoding();

            int bytesRead = 0;

            do
            {
                bytesRead = networkStream.Read(message, 0, message.Length);
            } 
            while (networkStream.DataAvailable);

            string rcvMessage = encoder.GetString(message, 0, bytesRead);

            IncomingUserMessage(this, new IncomingMessageEventArgs(rcvMessage, null));

            return bytesRead;
        }

        public int SendMessageAsync(IFowaMessage fowaMessage)
        {
            try
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(fowaMessage.Message.Trim());
                ClientStream.WriteAsync(buffer, 0, buffer.Length);
                return 0;
            }
            catch (Exception)
            {
                // Log exceptions
                return -1;
            }
        }

        protected void Dispose(bool freeManagedObjectsAlso)
        {
            //Free unmanaged resources here

            //Free managed resources too, but only if i'm being called from Dispose
            //(If i'm being called from Finalize then the objects might not exist anymore)
            if (!freeManagedObjectsAlso) return;

            if (_client != null)
            {
                _client.Close();
                _client = null;
            }

            if (ClientStream != null)
            {
                ClientStream.Close();
                ClientStream = null;
            }
        }

        public void Dispose()
        {
            Dispose(true); //i am calling you from Dispose, it's safe
            GC.SuppressFinalize(this); //Hey, GC: don't bother calling finalize later
        }

        ~FowaClient()
        {
            Dispose(false); //i am *not* calling you from Dispose, it's *not* safe
        }
    }
}
