using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FowaProtocol;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;
using FowaProtocol.FowaModels;
using FowaProtocol.MessageEnums;
using FowaProtocol.XmlDeserialization;
using Server.BL.Services;
using Server.DL;

namespace Server.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FowaMetaData _metaData;
        private readonly FowaService _service;
        private readonly UserFriendsService _userFriendService;

        public MainWindow()
        {
            _metaData = new FowaMetaData
                            {
                                OnIncomingLoginMessageCallback = OnIncomingLoginMessage,
                                OnIncomingUserMessageCallback = OnIncomingUserMessage,
                                OnIncomingRegisterMessageeCallback = OnIncomingRegisterMessage
                            };

            _service = new FowaService(_metaData);
            _service.UserDisconnected += OnUserDiconnected;
            _userFriendService = new UserFriendsService();

            InitializeComponent();
            _service.StartServer();
        }

        private void OnUserDiconnected(object sender, UserDisconnetedEventArgs e)
        {
            return;
        }

        public async void OnIncomingRegisterMessage(object sender, IncomingMessageEventArgs args)
        {
            var registerInfo = XmlDeserializer.GetRegisterInfo(args.Message);
            await Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Incoming register message:\n" + '\t' + registerInfo.NickName + "\n\t" + registerInfo.Email + "\n\t" + registerInfo.Pw + "\n\n"));

        }

        public async void OnIncomingUserMessage(object sender, IncomingMessageEventArgs args)
        {
            await Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Incoming UserMessage:\n"));
            string message = args.Message;

            var messageReceiver = XmlDeserializer.GetUserFromUserMessage(message, UserMessageElement.Receiver);
            var messageSender = XmlDeserializer.GetUserFromUserMessage(message, UserMessageElement.Sender);

            var userClient = _service.Clients.FirstOrDefault(c => c.Key == messageReceiver.UserId);

            if (userClient.Value == null) return; // User is not online send ErrorMessage
            var fowaClient = userClient.Value;

            //forward Usermessage
            bool forwardingToClientSuccesful = await fowaClient.WriteToClientStreamAync(new UserMessage(messageSender, messageReceiver, XmlDeserializer.GetMessage(args.Message)));

        }

        public async void OnIncomingLoginMessage(object sender, IncomingMessageEventArgs args)
        {
            // I will try to translate this comment later, but its just a note for me .. sry :D
            // folgendes würde zu folgender Aushahme führen:
            // Der aufrufende Thread kann nicht auf dieses Objekt zugreifen, da sich das Objekt im Besitz eines anderen Threads befindet."
            // fowaServerLogTextBlock.Text = args.Message;

            var login = XmlDeserializer.GetLoginInfo(args.Message);

            await Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Incoming Login:\n" + '\t' + login.Email + "\n\t" + login.Pw + "\n\n"));

            bool writeToClientSuccessfull; // Not used yet

            // DB con failed = 0
            // user exists = 1
            // User does not exists = 2
            var handleResult = await HandleIncomingLoginOrRegisterMessage(args.FowaClient, login.Email);

            if(handleResult == 0 || handleResult == 2) return; // if user exists .. carry on

            // Check if password is correct
            var user = _userFriendService.GetUserbyEmail(login.Email);

            if (!user.pw.Equals(login.Pw))
            {
                await Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Login failed: Wrong Password.\n----------\n"));
                writeToClientSuccessfull = await args.FowaClient.WriteToClientStreamAync(new ErrorMessage(ErrorMessageKind.LiginError, "Incorrect Password"));
                return;
            }

            // add user to Clientlist
            bool addingSuccessful = _service.Clients.TryAdd(user.ID, args.FowaClient);
            _service.RecentlyConnectedClient.ClientUserId = user.ID;

            // check if adding was successful

            // Send Friendlist to user
            var friends = _userFriendService.GetFriends(user);
            List<Friend> friendList = friends.Select(friend => new Friend { Email = friend.email, Nick = friend.nick, UserId = friend.ID }).ToList();
            FriendListMessage m = new FriendListMessage(new Friend { Email = user.email, Nick = user.nick, UserId = user.ID }, friendList);

            // send FriendListMessage
            writeToClientSuccessfull = await args.FowaClient.WriteToClientStreamAync(m);
        }

        public async Task<int> HandleIncomingLoginOrRegisterMessage(FowaClient client, string email)
        {
            bool writeToClientSuccessfull = true; // Not used yet
            bool userExists = true;
            bool dbConnectionSuccessfull = true;
            string possibleException = string.Empty;

            try
            {
                // Check if user exists
                userExists = _userFriendService.UserExists(email);
            }
            catch (Exception ex)
            {
                // Log ex
                possibleException = ex.Message;
                dbConnectionSuccessfull = false;
            }

            if(!userExists)
            {
                await Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Login failed: User not found.\n----------\n"));
                writeToClientSuccessfull = await client.WriteToClientStreamAync(new ErrorMessage(ErrorMessageKind.LiginError, "User not found."));
            }

            if (!dbConnectionSuccessfull)
            {
                await Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "DB connection failed.\n----------\n" + possibleException));
                writeToClientSuccessfull = await client.WriteToClientStreamAync(new ErrorMessage(ErrorMessageKind.LiginError, "Fetching Friends failed."));
            }

            if(!dbConnectionSuccessfull) return 0; // DB con failed
            return userExists ? 1 : 2; // user exists = 1
                                       // User does not exists = 2
        }

        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            //service.StartServer();
        }
    }
}
