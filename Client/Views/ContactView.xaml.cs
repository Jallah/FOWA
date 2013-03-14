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
using Client.ViewModels;
using FowaProtocol;


namespace Client.Views
{
    /// <summary>
    /// Interaktionslogik für ContactView.xaml
    /// </summary>
    public partial class ContactView : Window
    {
       
        public ContactView()
        {
            InitializeComponent();
        }

        //private void ListeItemDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    int index = listBox.SelectedIndex;
        //    string chatWith = list.ElementAt(index).Nick;

        //    if (listBox.SelectedIndex < 0) return;

        //    var chatWindow = (from w in HandledChatWindows
        //                      where w.Key == chatWith
        //                      select w.Value).FirstOrDefault();
        //    // Wenn kein Chatfenster für den User Existiert --> ChantWindow erstellen
        //    if (chatWindow == null)
        //    {
        //        HandledChatWindows.Add(chatWith, new ChatView(chatWith));
        //        HandledChatWindows.First(w => w.Key == chatWith).Value.ShowDialog();//Visibility = Visibility.Visible;
        //    }
        //    // Chatfenster aus Taskleist holen
        //    else
        //    {
        //        //var chantWindow = HandledChatWindows.First(w => w.Key == chatWith).Value;
        //        chatWindow.WindowState = WindowState.Normal;
        //        chatWindow.Activate();
        //    }
        //}
    }
}
