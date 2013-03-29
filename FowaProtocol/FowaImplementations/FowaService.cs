using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.EventArgs;

namespace FowaProtocol.FowaImplementations
{
    public class FowaService : IDisposable
    {
        private TcpListener _tcpListener;
        private bool _serviceIsRunning;
        private bool ServerIsRunning { get { return _serviceIsRunning; } }
        private readonly Task _listenTask;
        private readonly FowaMetaData _metaData;

        private readonly ConcurrentDictionary<int, FowaClient> _clients;
        public ConcurrentDictionary<int, FowaClient> Clients{get { return _clients; }}

        //private readonly BlockingCollection<ClientHandling> _clients;
        //public BlockingCollection<ClientHandling> Clients { get { return _clients; } } 

        public FowaService(FowaMetaData metaData)
            : base()
        {
            _metaData = metaData;
            this._tcpListener = new TcpListener(IPAddress.Any /*IPAddress.Parse("127.0.0.1")*/, 3333);
            this._listenTask = new Task(ListenForClients);
            _clients = new ConcurrentDictionary<int, FowaClient>();
            //_clients = new BlockingCollection<ClientHandling>();
        }

        public async void ListenForClients()
        {
            while (ServerIsRunning)
            {
                TcpClient tcpClient = await this._tcpListener.AcceptTcpClientAsync();

                NetworkStream stream = tcpClient.GetStream();

                ClientHandling client = new ClientHandling(stream);
                
                SubscribeEvents(client);

                client.StartReadingAsync();

                //Clients.Add(client);
            }
        }

        public void StartServer()
        {
            _serviceIsRunning = true;
            if (_listenTask.Status != TaskStatus.Running) _listenTask.Start();
            _tcpListener.Start();
        }

        public void StopServer()
        {
            _tcpListener.Stop();
            _serviceIsRunning = false;
        }

        public void Dispose(bool freeManagedObjectsAlso)
        {
            // Free unmanaged Code
            // here

            if (!freeManagedObjectsAlso) return;

            // Free managed Code
            // here
            if (_tcpListener != null)
            {
                _tcpListener.Stop();
                _tcpListener = null;
            }

            foreach (var client in _clients.Where(client => client.Value != null))
            {
                client.Value.Dispose();
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

        private void SubscribeEvents(ClientHandling client)
        {
            client.IncomingLoginMessage += _metaData.OnIncomingLoginMessageCallback;
            client.IncomingRegisterMessage += _metaData.OnIncomingRegisterMessageeCallback;
            client.IncomingUserMessage += _metaData.OnIncomingUserMessageCallback;
            client.IncomingSeekFriendsRequestMessage += _metaData.OnIncomingSeekFriendsRequestMessageCallback;
            client.IncomingErrorMessage += _metaData.OnIncomingErrorMessageCallback;
            client.IncomingFriendlistMessage += _metaData.OnIncomingFriendlistMessageCallback;
        }

    }
}
