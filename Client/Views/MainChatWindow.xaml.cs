using System.Windows;

namespace Client.Views
{
    /// <summary>
    /// Interaktionslogik für MainChatWindow.xaml
    /// </summary>
    public partial class MainChatWindow : Window
    {
        public MainChatWindow()
        {
            InitializeComponent();
            DataContext = App.MainChatWindowViewModel;
            //this.ChatTab.ItemsSource = App.MainChatWindowViewModel.UserChats;
        }
    }
}
