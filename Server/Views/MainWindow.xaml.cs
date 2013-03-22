using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            _metaData = new FowaMetaData { OnIncomingLoginMessageCallback = this.OnIncomingLoginMessage };
            _service = new FowaService(_metaData);
            _userFriendService = new UserFriendsService();

            InitializeComponent();
            //service.IncomingUserMessage += OnIncomingUserMessage;
            _service.StartServer();
        }

        public async void OnIncomingLoginMessage(object sender, IncomingMessageEventArgs args)
        {
            // folgendes würde zu folgender Aushahme führen:
            // Der aufrufende Thread kann nicht auf dieses Objekt zugreifen, da sich das Objekt im Besitz eines anderen Threads befindet."
            // fowaServerLogTextBlock.Text = args.Message;
#if DEBUG
            List<Friend> testList = new List<Friend>();
            Friend f = new Friend{Email = "friend@gmx.net", Nick = "Friend 1", UserId = 23};
            Friend f2 = new Friend{Email = "friend2@google.com", Nick = "Friend 2", UserId = 233};
            testList.Add(f);
            testList.Add(f);
            FriendListMessage me = new FriendListMessage(new User { Email = "degug@test.de", LastMessage = DateTime.Now, Nick = "Testuser" }, testList);
            bool ok = await args.FowaClient.WriteToClientStreamAync(me);
            return;
#endif



            var login = XmlDeserializer.GetLoginInfo(args.Message);
            Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Incoming Login:\n" + '\t' + login.Email + "\n\t" + login.Pw + "\n\n"));

            // Check if user exists
            if (!_userFriendService.UserExists(login.Email))
            {
                Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Login failed: User not found.\n----------\n"));
                bool successful = await args.FowaClient.WriteToClientStreamAync(new ErrorMessage(ErrorMessageKind.LiginError, "User not found."));
                return;
            }

            // Check if password is correct
            var user = _userFriendService.GetUserbyEmail(login.Email);

            if (!user.pw.Equals(login.Pw))
            {
                Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text += "Login failed: Wrong Password.\n----------\n"));
                bool successfull = await args.FowaClient.WriteToClientStreamAync(new ErrorMessage(ErrorMessageKind.LiginError, "Incorrect Password"));
                return;
            }

            // Send Friendlist to user
            var friends = _userFriendService.GetFriends(user);
            List<Friend> friendList = friends.Select(friend => new Friend { Email = friend.email, Nick = friend.nick, UserId = friend.ID }).ToList();
            FriendListMessage m = new FriendListMessage(new User { Email = user.email, LastMessage = user.lastMessage, Nick = user.nick, UserId = user.ID }, friendList);

            // send FriendListMessage
            bool success = await args.FowaClient.WriteToClientStreamAync(m);
        }


        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            //service.StartServer();
        }
    }
}
