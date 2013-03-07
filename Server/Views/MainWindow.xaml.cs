using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol;
using FowaProtocol.FowaMessages;

namespace FOWA.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FowaMetaData _metaData;
        private readonly FowaService _service; 

        public MainWindow()
        {
            _metaData = new FowaMetaData {OnIncomingLoginMessageCallback = this.OnIncomingLoginMessage};
            _service = new FowaService(_metaData);

            InitializeComponent();
            //service.IncomingUserMessage += OnIncomingUserMessage;
            _service.StartServer();
        }


        public void OnIncomingLoginMessage(object sender, IncomingMessageEventArgs args)
        {
            // folgendes würde zu folgender Aushahme führen:
            // Der aufrufende Thread kann nicht auf dieses Objekt zugreifen, da sich das Objekt im Besitz eines anderen Threads befindet."
            // fowaServerLogTextBlock.Text = args.Message;

            Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text = args.Message));
            StreamWriter sw = new StreamWriter(args.SenderNetworkStream);
            List<User> l = new List<User>
                               {
                                   new User(234, "","","", "")
                               }; 
            FriendListMessage<User> m = new FriendListMessage<User>(l);
            sw.WriteLineAsync(m.Message);
            sw.Flush();
        }


        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            //service.StartServer();
        }
    }
}
