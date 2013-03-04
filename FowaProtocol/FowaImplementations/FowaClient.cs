using System;
using System.Collections.Generic;
using System.IO;
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

        public bool WriteToClientStreamAync(IFowaMessage fowaMessage, NetworkStream networkStream)
        {
            bool successfull = true;

            try
            {
                //ASCIIEncoding encoder = new ASCIIEncoding();
                //byte[] buffer = encoder.GetBytes(fowaMessage.Message.Trim());
                StreamWriter sw = new StreamWriter(networkStream);
                sw.WriteAsync(fowaMessage.Message.Trim());
                //networkStream.WriteAsync(buffer, 0, buffer.Length);

            }
            catch (Exception)
            {
                // Log exceptions
                successfull = false;
            }

            return successfull;
        }

        public void ReadFromClientSream(NetworkStream networkStream)
        {
            StreamReader sr = new StreamReader(networkStream);
            Task<string> rvcMessage = sr.ReadToEndAsync();
            IncomingUserMessage(this, new IncomingMessageEventArgs(rvcMessage.Result, null));
        }

        public void SendMessageAsync(IFowaMessage fowaMessage)
        {
            //ASCIIEncoding encoder = new ASCIIEncoding();
            //byte[] buffer = encoder.GetBytes(fowaMessage.Message.Trim());
            //ClientStream.WriteAsync(buffer, 0, buffer.Length);
            StreamWriter sw = new StreamWriter(ClientStream);
            sw.WriteAsync(fowaMessage.Message.Trim());
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
