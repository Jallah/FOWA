using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using FowaProtocol.FowaModels;

namespace Client.ViewModels
{
    public class ContactViewModel : ViewModelBase.ViewModelBase
    {
        #region Fields

        public BindableCollection<Friend> Friends { get; private set; }
        private List<int> OpenTabs { get; set; } 
        private MainChatViewModel MainChatViewModel { get; set; }
        private Window MainChatView { get; set; }
        private readonly IWindowManager _windowManager;

        #endregion

        #region Ctor

        public ContactViewModel(IWindowManager windowManager, IEnumerable<Friend> friends )
        {
            _windowManager = windowManager;
            Friends = new BindableCollection<Friend>(friends);
            OpenTabs = new List<int>();
        }

        #endregion

        #region OpenUserChat

        public void OpenUserChat(Friend friend)
        {
            if (MainChatViewModel == null) MainChatViewModel = new MainChatViewModel();
 
            if (!MainChatViewModel.IsActive)
            {
                _windowManager.ShowWindow(MainChatViewModel);
                MainChatView = MainChatViewModel.GetView() as Window;
            }
            else
            {
                if (MainChatView != null)
                {
                    if(MainChatView.WindowState == WindowState.Minimized) MainChatView.WindowState = WindowState.Normal;
                    MainChatView.Activate();
                }
            }

           

            var tab = (from f in OpenTabs where f == friend.UserId select f).FirstOrDefault();

            // tab does exist
            if (tab != 0) return;

            MainChatViewModel.OpenTab(this,friend);
            OpenTabs.Add(friend.UserId);
        }

        #endregion

        #region remove Tab

        public void RemoveTab(int uid)
        {
            OpenTabs.Remove(uid);
            if (OpenTabs.Count == 0)
                if(MainChatView != null) MainChatView.Close();
        }

        #endregion
    }
}
