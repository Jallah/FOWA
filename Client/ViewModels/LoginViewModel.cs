using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Client.CommandBase;
using Client.Commands;
using Client.Helper;
using Client.SingletonFowaClient;
using Client.Views;
using FowaProtocol;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;
using FowaProtocol.FowaModels;
using FowaProtocol.XmlDeserialization;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Client.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : ViewModelBase.ViewModelBase, IViewAware
    {

        #region Fields

        private readonly IWindowManager _windowManager;
        private string _eMail;
        private string _password;
        private string _info;
        private readonly FowaConnection _connection;

        #endregion

        #region Ctor

        [ImportingConstructor]
        public LoginViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _connection = FowaConnection.Instance;
            _connection.ConnectionFailed += OnConnectionFailed;
            FowaMetaData data = new FowaMetaData { OnIncomingFriendlistMessageCallback = OnIncomingFriendlistMessage, OnIncomingErrorMessageCallback = OnIncomingErrorMessage};
            _connection.FowaMetaData = data;
        }

        #endregion

        #region EventHandler

        // login successful
        public void OnIncomingFriendlistMessage(object sender, IncomingMessageEventArgs e)
        {
            _connection.LoggedInAs = XmlDeserializer.GetLoggedInAsInfo(e.Message);

            var list = XmlDeserializer.DeserializeFriends(e.Message);

            _windowManager.ShowWindow(new ContactViewModel(_windowManager, list));

            CloseView();
            //string fr = list.Aggregate("", (current, friend) => current + friend.Email + " " + friend.Nick + " " + friend.UserId + '\n');
        }

        public void OnIncomingErrorMessage(object sender, IncomingErrorMessageEventArgs e)
        {
            Info = XmlDeserializer.GetMessage(e.Message);
        }

        public void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Info = string.Empty;
            Execute.OnUIThread(() => _windowManager.ShowDialog(new ErrorViewModel(e.Exception.Message)));
        }

        #endregion

        #region Properties
        public string EMail
        {
            get { return _eMail; }
            set
            {
                if (_eMail == value) return;
                _eMail = value;
                OnPropertyChanged(this, "EMail");
                OnPropertyChanged(this, "CanSendLoginData");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged(this, "Password");
                OnPropertyChanged(this, "CanSendLoginData");
            }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                if (_info == value) return;
                _info = value;
                OnPropertyChanged(this, "Info");
            }
        }
        #endregion

        #region not used Commands
        //public CommandModel SendLoginDataCommand
        //{
        //    get { return _sendLogin; }
        //}

        //public CommandModel OpenRegisterDialogCommand
        //{
        //    get { return _openRegisterDialog; }
        //}
        #endregion

        #region SendLoginData and wait for server response

        public async void SendLoginData()
        {
            string pw = Password;
            Password = string.Empty;
            Info = "Please wait ...";

            if (!_connection.Connected())
            {
                bool connected = await Task.Run(() => _connection.Connect());

                if (!connected) return;
            }
            
            var successful = await _connection.WriteToClientStreamAync(new LoginMessage(EMail.Trim().ToLower(), pw.Trim()));

            if (!successful)
            {
                _windowManager.ShowDialog(new ErrorViewModel("Service not available"));
                return;
            }

            StartReadingServerResponseAsync();
        }

        public async void StartReadingServerResponseAsync()
        {
            try
            {
                string s = await _connection.ReadFromStreamAsync();
                _connection.HandleIncomingMessage(s, _connection.ClientStream);
            }
            catch (Exception ex)
            {
                _windowManager.ShowWindow(new ErrorViewModel(ex.Message));
            }
        }

        public bool CanSendLoginData
        {
            get { return (Validator.IsEmail(this.EMail) && Validator.ValidateFields(new[] { this.EMail, this.Password })); }
        }
        #endregion

        #region OpenRegisterDialog
        public void OpenRegisterDialog()
        {
            //CloseView();
            _windowManager.ShowWindow(new MainChatViewModel());
        }
        #endregion

        #region IDataErrorInfo implementation
        public override string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;
                switch (columnName)
                {
                    case "EMail":
                        if (!Validator.IsEmail(EMail)) errorMessage = "Invalid e-mail";
                        break;
                    case "Password":
                        break;
                }

                return errorMessage;
            }
        }
        #endregion

        #region IViewAware implementation

        private Window _loginView;

        public void AttachView(object view, object context = null)
        {
            _loginView = view as Window;
            if (ViewAttached != null)
                ViewAttached(this, new ViewAttachedEventArgs() { Context = context, View = view });
        }

        public object GetView(object context = null)
        {
            return _loginView;
        }

        public void CloseView()
        {
            _loginView.Close();
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        #endregion
    }
}
