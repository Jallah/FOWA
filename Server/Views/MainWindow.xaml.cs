using System;
using System.Windows;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol;

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
            _metaData = new FowaMetaData {OnIncomingUserMessageCallback = this.OnIncomingUserMessage};
            _service = new FowaService(_metaData);

            InitializeComponent();
            //service.IncomingUserMessage += OnIncomingUserMessage;
            _service.StartServer();
        }


        public void OnIncomingUserMessage(object sender, IncomingMessageEventArgs args)
        {
            // folgendes würde zu folgender Aushahme führen:
            // Der aufrufende Thread kann nicht auf dieses Objekt zugreifen, da sich das Objekt im Besitz eines anderen Threads befindet."
            // fowaServerLogTextBlock.Text = args.Message;

            Dispatcher.BeginInvoke(new Action(() => fowaServerLogTextBlock.Text = args.Message));
        }


        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            //service.StartServer();
        }
    }
}
