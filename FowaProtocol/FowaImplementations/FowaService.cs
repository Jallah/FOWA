﻿using System;
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
        private FowaMetaData _metaData;

        public List<ClientHandling> Clients;

        public FowaService(FowaMetaData metaData)
            : base()
        {
            _metaData = metaData;
            this._tcpListener = new TcpListener(IPAddress.Any /*IPAddress.Parse("127.0.0.1")*/, 3000);
            this._listenTask = new Task(ListenForClients);
            Clients = new List<ClientHandling>();
        }

        public async void ListenForClients()
        {
            TcpClient tcpClient = await this._tcpListener.AcceptTcpClientAsync();

            NetworkStream stream = tcpClient.GetStream();

            ClientHandling client = new ClientHandling(stream);

            SubscribeEvents(client);

            client.StartReadingAsync();

            Clients.Add(client);
        }

        public void StartServer()
        {
            if (_listenTask.Status != TaskStatus.Running) _listenTask.Start();
            _tcpListener.Start();
            _serviceIsRunning = true;
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

            foreach (var client in Clients.Where(client => client != null))
            {
                client.Stream.Dispose();
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

         //OnIncomingLoginMessageEventHandler(object sender, IncomingMessageEventArgs args);
         //OnIncomingRegisterMessageEventHandler(object sender, IncomingMessageEventArgs args);
         //OnIncomingUserMessageEventHandler(object sender, IncomingMessageEventArgs args);
         //OnIncomingSeekFriendsRequestMessageEventHandler(object sender, IncomingMessageEventArgs args);
         //OnIncomingErrorMessageMessageEventHandler(object sender, IncomingErrorMessageEventArgs args);
         //OnIncomingFriendlistMessageEventHandler(object sender, IncomingMessageEventArgs args);

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
