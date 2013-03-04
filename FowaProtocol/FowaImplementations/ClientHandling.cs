using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.FowaImplementations
{
    public class ClientHandling : FowaProtocol
    {
        private readonly StreamReader _streamReader;

        public NetworkStream Stream { get; set; }

        public ClientHandling(NetworkStream networstream)
        {
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
                    HandleIncomingMessage(s, Stream);
                }
                catch (Exception exception)
                {
                    // Client Disconnected
                    
                    // log Exception
                    //Console.WriteLine("Client {0} disconnected", _nr);
                    break;
                }

            }
        }
    }
}
