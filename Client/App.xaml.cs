using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Client.ViewModels;
using Client.Views;

namespace Client
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static ContactWindow ContactWindow;

        private static MainChatWindowViewModel _mainChatWindowViewModel;

        public static MainChatWindowViewModel MainChatWindowViewModel
        {
            get { return _mainChatWindowViewModel ?? (_mainChatWindowViewModel = new MainChatWindowViewModel()); }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //ChatWindow chatWindow = new ChatWindow();
            //chatWindow.Show();

            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.Show();

            //RegisterWindow registerWindow = new RegisterWindow();
            //registerWindow.Show();

            //MainChatWindow mainChat = new MainChatWindow();
            //mainChat.Show();

            //ContactWindow = new ContactWindow();
            //ContactWindow.Show();
        }
    }
}
