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
using MessageBox = System.Windows.Forms.MessageBox;

namespace Client.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : ViewModelBase.ViewModelBase
    {

        #region Fields
        private readonly IPEndPoint _ip = new IPEndPoint(IPAddress.Parse(Settings.ClientSettings.Default.FowaServerIp), Settings.ClientSettings.Default.FowaServerPort);
        private readonly IWindowManager _windowManager;
        private string _eMail;
        private string _password;
        private string _info;
       // private readonly FowaClient _client;
        private FowaConnection connection;

        //private readonly CommandModel _sendLogin;
        //private readonly CommandModel _openRegisterDialog;
        
        #endregion

        #region Ctor
        [ImportingConstructor]
        public LoginViewModel(IWindowManager windowManager)
        {
            //_sendLogin = new SendLoginDataCommand(this);
            //_openRegisterDialog = new OpenDialogCommand(this);
            _windowManager = windowManager;

            connection = FowaConnection.Instance;

            connection.ConnectionFailed += OnConnectionFailed;
            FowaMetaData data = new FowaMetaData {OnIncomingFriendlistMessageCallback = OnIncomingFriendlistMessage};
            connection.FowaMetaData = data;
        }
        #endregion

        #region EventHandler

        public void OnIncomingFriendlistMessage(object sender, IncomingMessageEventArgs e)
        {
            var list = FowaProtocol.XmlDeserialization.XmlDeserializer.DeserializeFriends(e.Message);

            string fr = list.Aggregate("", (current, friend) => current + friend.Email + " " + friend.Nick + " " + friend.UserId + '\n');

            MessageBox.Show(fr);
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

            if(!connection.Connected())
            {
                bool connected = await Task.Run(() => connection.Connect());
                
                if (!connected) return;
            }

            var successful = await connection.WriteToClientStreamAync(new LoginMessage(EMail, pw));

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
                string s = await connection.ReadFromStreamAsync();
                connection.HandleIncomingMessage(s, connection.ClientStream);
            }
            catch (Exception ex)
            {
                _windowManager.ShowWindow(new ErrorViewModel(ex.Message));
            }
            
        }

        public bool CanSendLoginData
        {
            get { return (Validator.IsEmail(this.EMail) && Validator.ValidateFields(new[] {this.EMail, this.Password})); }
        }
        #endregion

        #region OpenRegisterDialog
        public void OpenRegisterDialog()
        {
           _windowManager.ShowDialog(new ErrorViewModel("hans"));
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
    }
}
