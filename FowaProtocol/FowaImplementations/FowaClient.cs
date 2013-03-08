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
    public class FowaClient : FowaProtocol, IDisposable
    {
        private Socket _client;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        public NetworkStream ClientStream { get; set; }

        // = new IPEndPoint(IPAddress.Parse(/*"127.0.0.1"*/"192.168.2.108"), 80);

        public FowaClient()
            : base()
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public FowaClient(NetworkStream stream)
        {
            ClientStream = stream;
            _streamWriter = new StreamWriter(ClientStream);
            _streamReader = new StreamReader(ClientStream);
        }

        public void Connect(IPEndPoint endPoint)
        {
            try
            {
                _client.Connect(endPoint);
                ClientStream = new NetworkStream(_client);
                _streamWriter = new StreamWriter(ClientStream);
                _streamReader = new StreamReader(ClientStream);
               
            }
            catch (Exception ex)
            {
                throw new Exception("Connection failed: " + ex.Message);
            }

        }

        public async Task<bool> WriteToClientStreamAync(IFowaMessage fowaMessage)
        {
            bool successfull = true;

            try
            {
                await _streamWriter.WriteLineAsync(fowaMessage.Message);
                _streamWriter.Flush();
            }
            catch (Exception)
            {
                // Log exceptions
                successfull = false;
            }

            return successfull;
        }

        public async Task<string> ReadFromSreamAsync()
        {
            string s = await _streamReader.ReadLineAsync();
            return s;
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
