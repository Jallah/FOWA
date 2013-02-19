using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FowaProtocol.FowaImplementation;
using FowaProtocol.FowaMessages;

namespace Client.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private const int MAX_CHARS = 200;
        public string ChatWith { get; private set; } 

        private FowaClient _client = new FowaClient();

        public ChatWindow(string chatWith)
        {
            InitializeComponent();
            try
            {
                _client.Connect(new IPEndPoint(IPAddress.Parse(/*"127.0.0.1"*/"192.168.2.108"), 3000));
            }
            catch (Exception ex)
            {
                this.chatTextBlock.Text = "Fehler:\n" + ex.Message;
            }
            
            charCounterNumberLabel.Content = MAX_CHARS;
            this.ChatWith = chatWith;
            this.Title = Title + " " + ChatWith;
        }

        private void inputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    chatTextBlock.Text = chatTextBlock.Text + inputTextBox.Text.Trim() + "\n";
                    _client.SendMessage(new UserMessage("hans", inputTextBox.Text.Trim()));
                    inputTextBox.Clear();
                    charCounterNumberLabel.Content = MAX_CHARS;
                    break;
                case Key.Back:
                    break;
                default:
                    if (MAX_CHARS - inputTextBox.Text.Count() <= 0)
                        e.Handled = true;
                    break;
            }
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MAX_CHARS - inputTextBox.Text.Count() >= 0)
                charCounterNumberLabel.Content = MAX_CHARS - inputTextBox.Text.Count();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _client.Dispose();
            _client = null;
            App.ContactWindow.HandledChatWindows.Remove(ChatWith);
        }
       


      
    }
}
