using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaImplementations
{
    public class FowaService : FowaProtocol, IDisposable
    {
        private TcpListener _tcpListener;
        private bool ServerIsRunning { get; set; }
        private readonly Task _listenTask;
        private readonly object _locker = new object();
        private const int BUFFER_SIZE = 4096; // 2^12
        private static int _connectedClients = 0;

        public Dictionary<int, Task> ClientTasks;
        public int ConnectedClients { get { return _connectedClients; } }
        
        
        public FowaService() : base()
        {
            this._tcpListener = new TcpListener(IPAddress.Any /*IPAddress.Parse("127.0.0.1")*/, 3000);
            this._listenTask = new Task(ListenForClients);
            ClientTasks = new Dictionary<int, Task>();
        }

        private void ListenForClients()
        {
            this._tcpListener.Start();

            while(ServerIsRunning)
            {
                TcpClient client = _tcpListener.AcceptTcpClient();
                if (!ServerIsRunning) return;

                lock (_locker)
                {
                    _connectedClients++;
                }

                Task clientHandleTask = new Task(() => HandleClientComm(client));
                clientHandleTask.Start();

                ClientTasks.Add(_connectedClients, clientHandleTask);
            }

            lock (_locker)
            {
                _connectedClients = 0;
            }
        }

        private void HandleClientComm(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();
            //Client cl = new Client(ClientCount, ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString(), ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port.ToString(), "hans");

            byte[] message = new byte[BUFFER_SIZE];

            while (true)
            {
                int bytesRead = 0;

                try
                {
                    // The Read call will block indefinitely until a message from the client has been received. 
                    // If you read zero bytes from the client, you know the client has disconnected. Otherwise, a message
                    // has been successfully received from the server.

                    //Incoming message may be larger than the buffer size.
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, message.Length);
                }
                catch
                {
                    //a socket error has occured
                    // Verbindung mit Client unterbrochen
                    client.Close();
                    clientStream.Close();

                    //var task = (from obj in _clientTasks
                    //            where obj.Key == cl.ClNo
                    //            select obj.Value).FirstOrDefault();
                    // task.CancelTokenSource.Cancel();

                    //Console.WriteLine(task.ClTask.Status);
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    //Console.WriteLine("Verbindung mit Client Nr. " + cl.ClNo + "beendet. --> bytesRead == 0");
                    client.Close();
                    clientStream.Close();
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                string rcvMessage = encoder.GetString(message, 0, bytesRead);
                HandleIncomingMessage(rcvMessage, clientStream);
                
                // message to the client
                //byte[] buffer = encoder.GetBytes("Hello Client!");

                //clientStream.Write(buffer, 0, buffer.Length);
                //Die Flush-Methode implementiert die Stream.Flush-Methode, wirkt sich jedoch nicht auf Netzwerkstreams aus, da der NetworkStream
                //nicht gepuffert ist.Durch Aufrufen der Flush-Methode wird keine Ausnahme ausgelöst.
                clientStream.Flush();
            }

            lock (_locker)
            {
                _connectedClients--;
            }

            client.Close();
            clientStream.Close();
        }

        public void StartServer()
        {
            if (_listenTask.Status != TaskStatus.Running) _listenTask.Start();
            _tcpListener.Start();
            ServerIsRunning = true;
        }

        public void StopServer()
        {
            _tcpListener.Stop();
            ServerIsRunning = false;
        }

        protected void Dispose(bool freeManagedObjectsAlso)
        {
            // Free unmanaged Code
            // here

            if (!freeManagedObjectsAlso) return;

            // Free managed Code
            if(_tcpListener != null)
            {
                _tcpListener.Stop();
                _tcpListener = null;
            }

            foreach (var clientTask in ClientTasks.Where(clientTask => clientTask.Value != null))
            {
                clientTask.Value.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        ~FowaService()
        {
            Dispose(false); // false because otherwise the managed Code will be (tried to) dispose twice
        }
    }
}
