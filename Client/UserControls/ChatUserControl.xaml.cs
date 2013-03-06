using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;

namespace Client.UserControls
{
    /// <summary>
    /// Interaktionslogik für ChatUserControl.xaml
    /// </summary>
    public partial class ChatUserControl : UserControl
    {

        private const int MAX_CHARS = 200;
        public string ChatWith { get; set; }
        private readonly FowaClient _client = new FowaClient(new IPEndPoint(IPAddress.Parse( /*"127.0.0.1"*/"192.168.2.108"), 3000));

        public ChatUserControl(/*string chatWith*/)
        {
            InitializeComponent();
            charCounterNumberLabel.Content = MAX_CHARS;
            //this.ChatWith = chatWith;
            //this.Title = Title + " " + ChatWith;
            
            Task.Factory.StartNew(() =>
            {
                try
                {
                    _client.Connect(
                    new IPEndPoint(IPAddress.Parse( /*"127.0.0.1"*/"192.168.2.108"), 3000));
                }
                catch (Exception)
                {

                    Dispatcher.BeginInvoke(
                        new Action(() => this.chatTextBlock.Text = "Error:\n\nConnect with User failed."));
                }

            });
        }

        private void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    chatTextBlock.Text = chatTextBlock.Text + InputTextBox.Text.Trim() + "\n";
                   // _client.SendMessageAsync(new UserMessage("hans", InputTextBox.Text.Trim()));
                    InputTextBox.Clear();
                    charCounterNumberLabel.Content = MAX_CHARS;
                    break;
                case Key.Back:
                    break;
                default:
                    if (MAX_CHARS - InputTextBox.Text.Count() <= 0)
                        e.Handled = true;
                    break;
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MAX_CHARS - InputTextBox.Text.Count() >= 0)
                charCounterNumberLabel.Content = MAX_CHARS - InputTextBox.Text.Count();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    _client.Dispose();
        //    _client = null;
        //    App.ContactWindow.HandledChatWindows.Remove(ChatWith);
        //}
    }
}
