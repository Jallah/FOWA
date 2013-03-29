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

        public BindableCollection<Friend> Friends { get; set; }
        public List<int> OpenTabs { get; set; } 
        private MainChatViewModel MainChatViewModel { get; set; }
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
                _windowManager.ShowWindow(MainChatViewModel);
            else
            {
                var window = MainChatViewModel.GetView() as Window;
                if (window != null) window.Activate();
            }

            var user = Friends.FirstOrDefault(f => f.UserId == friend.UserId);
            if (user == null) return;

            var tab = (from f in OpenTabs where f == friend.UserId select f).FirstOrDefault();

            // tab does exist
            if (tab != 0) return;
            MainChatViewModel.OpenTab(friend);
            OpenTabs.Add(friend.UserId);
        }

        #endregion
    }
}
