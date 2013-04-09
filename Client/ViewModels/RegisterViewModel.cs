using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using Caliburn.Micro;
using Client.Helper;
using Client.SingletonFowaClient;
using FowaProtocol;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaMessages;
using FowaProtocol.FowaModels;
using FowaProtocol.XmlDeserialization;

namespace Client.ViewModels
{
    public class RegisterViewModel : ViewModelBase.ViewModelBase, IViewAware
    {

        #region Fields

        private string _nickName;
        private string _eMail;
        private string _password;
        private string _confirmedPassword;
        private string _info;
        private readonly FowaConnection _connection;
        private readonly IWindowManager _windowManager;

        #endregion

        #region Properties

        public string NickName
        {
            get { return _nickName; }
            set
            {
                if (_nickName == value) return;
                _nickName = value;
                OnPropertyChanged(this, "NickName");
                OnPropertyChanged(this, "CanSendRegisterMessage");
            }
        }

        public string Email
        {
            get { return _eMail; }
            set
            {
                if (_eMail == value) return;
                _eMail = value;
                OnPropertyChanged(this, "Email");
                OnPropertyChanged(this, "CanSendRegisterMessage");
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
                OnPropertyChanged(this, "CanSendRegisterMessage");
            }
        }

        public string ConfirmedPassword
        {
            get { return _confirmedPassword; }
            set
            {
                if (_confirmedPassword == value) return;
                _confirmedPassword = value;
                OnPropertyChanged(this, "ConfirmedPassword");
                OnPropertyChanged(this, "CanSendRegisterMessage");
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

        #region Ctor

        public RegisterViewModel(IWindowManager windowManager)
        {
            FowaMetaData metaData = new FowaMetaData
                                        {
                                            OnIncomingErrorMessageCallback = OnIncomingErrorMessage,
                                            OnIncomingFriendlistMessageCallback = OnIncomingFriendListMessage
                                        };
            Info = "Register info\n\tFill in the fields and register to be cool :D\n\t...";
            _windowManager = windowManager;
            _connection = FowaConnection.Instance;
            _connection.FowaMetaData = metaData;
        }

        #endregion

        #region EventHandler

        public void OnIncomingErrorMessage(object sender, IncomingErrorMessageEventArgs args)
        {
            Info = XmlDeserializer.GetMessage(args.Message);
        }

        // the server will send a (empty) friend list to indicate that the user has successfully registered.
        public void OnIncomingFriendListMessage(object sender, IncomingMessageEventArgs args)
        {
            _connection.LoggedInAs = XmlDeserializer.GetLoggedInAsInfo(args.Message);

            //you do not have to deserialize the incoming friendlist, you will not have friends yet :)
            //var list = XmlDeserializer.DeserializeFriends(args.Message);

            _windowManager.ShowWindow(new ContactViewModel(_windowManager, Enumerable.Empty<Friend>()));

            CloseView();
        }

        #endregion

        #region SendRegisterMessage

        public async void SendRegisterMessage()
        {
            var pw = Password;
            var cpw = ConfirmedPassword;

            Password = string.Empty;
            ConfirmedPassword = string.Empty;

            Info = "\n Please wait ...";

            if (!_connection.Connected())
            {
                bool connected = await Task.Run(() => _connection.Connect());

                if (!connected)
                {
                    Info = "Connection failed.";
                    return;
                }
            }

            var successful = await _connection.WriteToClientStreamAync(new RegisterMessage(Email, pw, NickName));

            if (successful) return;
            Info = "SORRY !!!\n\n\tService not available.\n\tPlease try again later.";
        }

        public bool CanSendRegisterMessage
        {
            get
            {
                return (Validator.IsEmail(Email) &&
                        Validator.ValidateFields(new[] { NickName, Email, Password, ConfirmedPassword }) &&
                        Password.Equals(ConfirmedPassword));
            }
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
                    case "Email":
                        if (!Validator.IsEmail(Email)) errorMessage = "Invalid e-mail";
                        break;
                    case "Password":
                        break;
                    case "ConfirmedPassword":
                        if (!Password.Equals(ConfirmedPassword)) errorMessage = "The passwords must match";
                        break;
                }

                return errorMessage;
            }
        }

        #endregion

        #region IViewAware implementation

        private Window _registerView;

        public void AttachView(object view, object context = null)
        {
            _registerView = view as Window;
            if (ViewAttached != null)
                ViewAttached(this, new ViewAttachedEventArgs() { Context = context, View = view });
        }

        public object GetView(object context = null)
        {
            return _registerView;
        }

        public void CloseView()
        {
            _registerView.Close();
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached;

        #endregion

    }
}
