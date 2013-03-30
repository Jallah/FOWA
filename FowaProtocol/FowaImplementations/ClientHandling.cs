using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.MessageEnums;

namespace FowaProtocol.FowaImplementations
{
    public class ClientHandling : FowaProtocol
    {
        private readonly StreamReader _streamReader;
        private readonly FowaService _fowaService;

        public int ClientUserId { get; set; }

        public NetworkStream Stream { get; set; }

        public ClientHandling(FowaService service, NetworkStream networstream)
        {
            _fowaService = service;
            Stream = networstream;
            _streamReader = new StreamReader(networstream);
        }

        public async void StartReadingAsync()
        {
            while (true)
            {
                try
                {
                    string s = await _streamReader.ReadLineAsync();
                    var messageKind = HandleIncomingMessage(s, Stream);
                }
                catch (Exception exception)
                {
                    // Client Disconnected -- remove Client from ServiceClientList
                    FowaClient client;

                    //When this method returns, contains the object removed from the System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>, or
                    //the default value of the TValue type if key does not exist. 
                    _fowaService.Clients.TryRemove(ClientUserId, out client);

                    string s = exception.Message;
#if DEBUG
                    Debug.WriteLine(s);
#endif
                    // log Exception
                    _streamReader.Close();
                    Stream.Close();
                    break;
                }

            }
        }
    }
}
