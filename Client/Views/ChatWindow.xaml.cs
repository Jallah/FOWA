using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FowaProtocol.FowaImplementations;
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
            charCounterNumberLabel.Content = MAX_CHARS;
            this.ChatWith = chatWith;
            this.Title = Title + " " + ChatWith;

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
                    _client.SendMessage(new UserMessage("hans", InputTextBox.Text.Trim()));
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _client.Dispose();
            _client = null;
            App.ContactWindow.HandledChatWindows.Remove(ChatWith);
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(InputTextBox);
        }

        private void Window_Activated_1(object sender, EventArgs e)
        {
            //if (InputTextBox.IsFocused) return;
            Keyboard.Focus(InputTextBox);
        }




    }
}
