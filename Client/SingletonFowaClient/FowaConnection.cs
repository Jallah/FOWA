using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;

namespace Client.SingletonFowaClient
{

   

    public class ConnectionFailedEventArgs : EventArgs
    {
        public Exception Exception;
        public ConnectionFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }

    // see http://www.ibm.com/developerworks/java/library/j-dcl/index.html
    // or http://www.yoda.arachsys.com/csharp/singleton.html

    public sealed class FowaConnection
    {
        private static volatile FowaConnection _instance;
        private static readonly object Lock = new Object();
        private FowaMetaData _fowaMetaData;
        private readonly IPEndPoint _ip = new IPEndPoint(IPAddress.Parse(Settings.ClientSettings.Default.FowaServerIp), Settings.ClientSettings.Default.FowaServerPort);

        public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;

        private readonly FowaClient _fowaClient;

        private FowaConnection()
        {
            _fowaClient = new FowaClient();
        }

        public static FowaConnection Instance
        {
            get
            {
                // double-check locking
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                            _instance = new FowaConnection();
                    }
                }

                return _instance;
            }
        }
        
        public FowaMetaData FowaMetaData
        {
            get { return _fowaMetaData; }

            set
            {
                var oldMetadata = _fowaMetaData;
               
                if(oldMetadata != null)
                {
                    _fowaClient.IncomingLoginMessage -= oldMetadata.OnIncomingLoginMessageCallback;
                    _fowaClient.IncomingRegisterMessage -= oldMetadata.OnIncomingRegisterMessageeCallback;
                    _fowaClient.IncomingUserMessage -= oldMetadata.OnIncomingUserMessageCallback;
                    _fowaClient.IncomingSeekFriendsRequestMessage -= oldMetadata.OnIncomingSeekFriendsRequestMessageCallback;
                    _fowaClient.IncomingErrorMessage -= oldMetadata.OnIncomingErrorMessageCallback;
                    _fowaClient.IncomingFriendlistMessage -= oldMetadata.OnIncomingFriendlistMessageCallback;
                }

                _fowaMetaData = value;
                _fowaClient.IncomingLoginMessage += _fowaMetaData.OnIncomingLoginMessageCallback;
                _fowaClient.IncomingRegisterMessage += _fowaMetaData.OnIncomingRegisterMessageeCallback;
                _fowaClient.IncomingUserMessage += _fowaMetaData.OnIncomingUserMessageCallback;
                _fowaClient.IncomingSeekFriendsRequestMessage +=_fowaMetaData.OnIncomingSeekFriendsRequestMessageCallback;
                _fowaClient.IncomingErrorMessage += _fowaMetaData.OnIncomingErrorMessageCallback;
                _fowaClient.IncomingFriendlistMessage += _fowaMetaData.OnIncomingFriendlistMessageCallback;
            }
        }

        public Task<bool> WriteToClientStreamAync(IFowaMessage message)
        {
            return _fowaClient.WriteToClientStreamAync(message);
        }

        public Task<string> ReadFromStreamAsync()
        {
            return _fowaClient.ReadFromStreamAsync();
        }

        public void HandleIncomingMessage(string message, NetworkStream stream)
        {
            _fowaClient.HandleIncomingMessage(message, stream);
        }

        public NetworkStream ClientStream
        {
            get { return _fowaClient.ClientStream; }
        }

        public bool Connected()
        {
            return _fowaClient.IsConnected();
        }

        public bool Connect()
        {
            try
            {
                 _fowaClient.Connect(_ip);
                return true;
            }
            catch (Exception ex)
            {
                // LogException(ex)
                if (ConnectionFailed != null)
                    ConnectionFailed(this, new ConnectionFailedEventArgs(ex));
                return false;
            }
        }

        
    }
}
