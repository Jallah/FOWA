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

        public ChatWindow()
        {
            InitializeComponent();
            charCounterNumberLabel.Content = MAX_CHARS;
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
    }
}
