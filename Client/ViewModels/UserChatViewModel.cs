using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Client.SingletonFowaClient;
using FowaProtocol;
using FowaProtocol.FowaMessages;
using FowaProtocol.FowaModels;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Screen = Caliburn.Micro.Screen;

namespace Client.ViewModels
{
    public  class UserChatViewModel : Screen
    {
        #region Fields

        private Friend User { get; set; }
        private ContactViewModel ContactViewModel { get; set; }
        private string _message;
        private string _chatContent;
        private int _charCounter;
        private readonly FowaConnection _connection;

        #endregion

        #region WriteToChat

        private void WriteToChat(string text)
        {
            ChatContent += text + "\n";
        }

        #endregion

        #region Properties

        public string Message
        {
            get { return _message; } 
            set
            {
                if(_message == value) return;
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public string ChatContent
        {
            get { return _chatContent; }
            set
            {
                if (_chatContent == value) return;
                _chatContent = value;
                NotifyOfPropertyChange(() => ChatContent);
            }
        }

        public int CharCounter
        {
            get { return _charCounter; }
            set
            {
                if (_charCounter == value) return;
                _charCounter = value;
                NotifyOfPropertyChange(() => CharCounter);
            }
        }

        #endregion

        #region Ctor

        public UserChatViewModel(ContactViewModel caller, Friend user)
        {
            base.DisplayName = user.Nick;
            ContactViewModel = caller;
            User = user;
            CharCounter = Settings.ClientSettings.Default.CharMax;
            _connection = FowaConnection.Instance;
        }

        #endregion

        #region EventHanlder

        public void OnTextChanged()
        {
            CharCounter = Settings.ClientSettings.Default.CharMax - Message.Count();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            ContactViewModel.RemoveTab(User.UserId);
        }

        // this is maybe not the best solution to accomplish key binding.
        // see: http://caliburnmicro.codeplex.com/discussions/222164
        // but its quick ;)
        public async void OnKeyDown(ActionExecutionContext context)
        {
            var eventArgs = (KeyEventArgs) context.EventArgs;
            var key = eventArgs.Key;

            if (key == Key.Enter)
            {
                Message = Message.Trim();
                if(!Message.Any()) return;

                var sendingSuccessful =
                    await _connection.WriteToClientStreamAync(new UserMessage(_connection.LogedInAs, User, Message));

                WriteToChat(_connection.LogedInAs.Nick + ": " + Message);

                // Check if sendinSuccessful is true and write it to the readonly textblock .. else write errormessage to readonly textblock.
                Message = string.Empty;
            }
            else if (key == Key.Delete || key == Key.Back)
            {
                return;
            }
            else
            {
                if(CharCounter <= 0)
                {
                    eventArgs.Handled = true;
                }
            }
        }

        #endregion
    }
}
