using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.FowaMessages;

namespace FowaProtocol.FowaImplementation
{
    public class FowaClient : IDisposable
    {
        private TcpClient _client;
        public NetworkStream ClientStream { get; set; }

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
            catch(Exception ex)
            {
                throw new Exception("Connection failed: " + ex.Message);
            }
            
        }

        public int SendMessage(IFowaMessage fowaMessage)
        {
            try
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(fowaMessage.Message.Trim());
                ClientStream.Write(buffer, 0, buffer.Length);
                return 0;
            }
            catch (Exception ex)
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

            if(_client != null)
            {
                _client.Close();
                _client = null;
            }
            
            if(ClientStream != null)
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
