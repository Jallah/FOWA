using System;
using System.Windows;
using FowaProtocol.FowaImplementation;
using FowaProtocol;

namespace FOWA.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FowaService service = new FowaService();

        public MainWindow()
        {
            InitializeComponent();
            service.IncomingUserMessage += OnIncomingUserMessage;
            service.StartServer();
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
