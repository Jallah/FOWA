using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Client.SingletonFowaClient;
using FowaProtocol;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaModels;
using FowaProtocol.XmlDeserialization;

namespace Client.ViewModels
{
    public class ContactViewModel : ViewModelBase.ViewModelBase
    {
        #region Fields

        public BindableCollection<IContact> Friends { get; private set; }
        private Dictionary<int, int> OpenTabs { get; set; }
        private MainChatViewModel MainChatViewModel { get; set; }
        private Window MainChatView { get; set; }
        private readonly IWindowManager _windowManager;
        private readonly FowaConnection _connection;
        private int TabIndex { get; set; }

        #endregion

        #region Ctor

        public ContactViewModel(IWindowManager windowManager, IEnumerable<IContact> friends)
        {
            _connection = FowaConnection.Instance;
            FowaMetaData metaData = new FowaMetaData { OnIncomingUserMessageCallback = OnIncomingUserMessage };
            _connection.FowaMetaData = metaData;
            _windowManager = windowManager;
            Friends = new BindableCollection<IContact>(friends);
            OpenTabs = new Dictionary<int, int>();
            StartListeningAsync();
        }

        #endregion

        #region Listen

        public async void StartListeningAsync()
        {
            while (true)
            {
                try
                {
                    string message = await _connection.ReadFromStreamAsync();
                    _connection.HandleIncomingMessage(message, _connection.ClientStream);
                }
                catch(Exception /*ex*/)
                {
                    // Log Exception
                    // Errormessage
                    break;
                }
            }
        }

        #endregion


        #region OpenUserChat

        public void OpenUserChat(IContact friend)
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
                    if (MainChatView.WindowState == WindowState.Minimized) MainChatView.WindowState = WindowState.Normal;
                    MainChatView.Activate();
                }
            }

            var tab = (from f in OpenTabs where f.Key == friend.UserId select f).FirstOrDefault();

            // tab does exist ... Key == UserId
            if (tab.Key != 0)
            {
                int tabIndex = OpenTabs.FirstOrDefault(t => t.Value == tab.Key).Value;
                MainChatViewModel.Items.ElementAt(tabIndex).Activate();
                return;
            }

            MainChatViewModel.OpenTab(this, friend);
            OpenTabs.Add(friend.UserId, MainChatViewModel.Items.Count - 1);
        }

        public void OnIncomingUserMessage(object sender, IncomingMessageEventArgs e)
        {
            // get the sender
            var user = XmlDeserializer.GetUserFromUserMessage(e.Message, UserMessageElement.Sender);

            var friend = Friends.FirstOrDefault(f => f.UserId == user.UserId);

            if (friend == null)
            {
                /* 
                 * user is not a Friend
                 * maybe add him as Friend
                */
            }

            OpenUserChat(friend); // now a tab is available

            // find the tab and write the incoming user message to the the chat box
            int tabIndex = OpenTabs.FirstOrDefault(t => t.Key == user.UserId).Value;
            UserChatViewModel tab = (UserChatViewModel)MainChatViewModel.Items.ElementAt(tabIndex);
            tab.WriteToChat(user.Nick + ": " + XmlDeserializer.GetMessage(e.Message));

        }

        #endregion

        #region remove Tab

        public void RemoveTab(int uid)
        {
            OpenTabs.Remove(uid);
            if (OpenTabs.Count == 0)
                if (MainChatView != null) MainChatView.Close();
        }

        #endregion

        #region activate Tab

        public void ActivateTab(int uid)
        {
            if (MainChatViewModel != null)
            {

            }
        }

        #endregion
    }
}
