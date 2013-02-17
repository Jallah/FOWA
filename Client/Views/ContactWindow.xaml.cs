﻿using System;
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

namespace Client.Views
{
    /// <summary>
    /// Interaktionslogik für ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        private Dictionary<string, ChatWindow> _handledChatWindows = new Dictionary<string, ChatWindow>();
        private ObservableCollection<Person> list;

        public ContactWindow()
        {
            InitializeComponent();

            list = new ObservableCollection<Person>
                       {
                           new Person() {Nachname = "Hans", Vorname = "Schuster"},
                           new Person() {Nachname = "Peter", Vorname = "Schwanzlurch"}
                       };

            listBox.ItemsSource = list;
        }

        private void addFriendButton_Click(object sender, RoutedEventArgs e)
        {
            list.ElementAt(1).Nachname = "Hurensohn";
        }

        private void ListeItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = listBox.SelectedIndex;

            if (listBox.SelectedIndex < 0) return;

            var chatWindow = (from w in _handledChatWindows
                              where w.Key == list.ElementAt(index).Nachname
                              select w.Value).FirstOrDefault();

            if(chatWindow == null)
            {
                _handledChatWindows.Add(list.ElementAt(index).Nachname, new ChatWindow());
                _handledChatWindows.First(w => w.Key == list.ElementAt(index).Nachname).Value.Visibility = Visibility.Visible;
            }
            else
            {
                _handledChatWindows.First(w => w.Key == list.ElementAt(index).Nachname).Value.WindowState = WindowState.Normal;
            }
        }

        

       
    }
}
