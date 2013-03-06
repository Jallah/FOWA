using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.ViewModel;
using FowaProtocol;


namespace Client.Views
{
    /// <summary>
    /// Interaktionslogik für ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public Dictionary<string, ChatWindow> HandledChatWindows = new Dictionary<string, ChatWindow>();
        public static SeekFriendsWindow SeekFriendsWindow;
        private ObservableCollection<UserChatViewModel> list;

        public ContactWindow()
        {
            InitializeComponent();

            list = new ObservableCollection<UserChatViewModel>
                       {
                           new UserChatViewModel(new User(234, "hans@penis.de", "Hans", "000.000.000.000", "")),
                           new UserChatViewModel(new User(555, "Peter@gmx.net", "Peter", "000.000.000.100", ""))
                       };

            listBox.ItemsSource = list;
        }

        private void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            if(SeekFriendsWindow == null)
            {
                SeekFriendsWindow = new SeekFriendsWindow();
                SeekFriendsWindow.Show();
            }
            else
            {
                SeekFriendsWindow.WindowState = WindowState.Normal;
            }
            
        }

        private void ListeItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = listBox.SelectedIndex;
            string chatWith = list.ElementAt(index).Nick;

            if (listBox.SelectedIndex < 0) return;

            var chatWindow = (from w in HandledChatWindows
                              where w.Key == chatWith
                              select w.Value).FirstOrDefault();
            // Wenn kein Chatfenster für den User Existiert --> ChantWindow erstellen
            if (chatWindow == null)
            {
                HandledChatWindows.Add(chatWith, new ChatWindow(chatWith));
                HandledChatWindows.First(w => w.Key == chatWith).Value.ShowDialog();//Visibility = Visibility.Visible;
            }
            // Chatfenster aus Taskleist holen
            else
            {
                //var chantWindow = HandledChatWindows.First(w => w.Key == chatWith).Value;
                chatWindow.WindowState = WindowState.Normal;
                chatWindow.Activate();
            }
        }
    }
}
