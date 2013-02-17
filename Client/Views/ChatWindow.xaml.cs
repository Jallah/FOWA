using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private const int MAX_CHARS = 200;
        public string ChatWith { get; private set; } 

        public ChatWindow(string chatWith)
        {
            InitializeComponent();
            charCounterNumberLabel.Content = MAX_CHARS;
            this.ChatWith = chatWith;
            this.Title = Title + " " + ChatWith;
        }

        private void inputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    chatTextBlock.Text = chatTextBlock.Text + inputTextBox.Text + "\n";
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

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.ContactWindow.HandledChatWindows.Remove(ChatWith);
        }
       


      
    }
}
