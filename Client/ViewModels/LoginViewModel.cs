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
using Client.Views;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;

namespace Client.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : ViewModelBase.ViewModelBase
    {

        #region Fields
        private readonly IWindowManager _windowManager;
        private string _eMail;
        private string _password;
        //private readonly CommandModel _sendLogin;
        //private readonly CommandModel _openRegisterDialog;
        private readonly FowaClient _client;
        #endregion

        #region Ctor
        [ImportingConstructor]
        public LoginViewModel(IWindowManager windowManager)
        {
            //_sendLogin = new SendLoginDataCommand(this);
            //_openRegisterDialog = new OpenDialogCommand(this);

            _windowManager = windowManager;
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
            _client = new FowaClient();
            _client.Connect(ip);
            _client.IncomingFriendlistMessage += OnIncomingFriendlistMessage;
        }
        #endregion

        #region EventHandler
        public void OnIncomingFriendlistMessage(object sender, IncomingMessageEventArgs e)
        {
            MessageBox.Show(e.Message);
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

        #region SendLoginData
        public async void SendLoginData()
        {
            await _client.WriteToClientStreamAync(new LoginMessage(EMail, Password));
            string s = await _client.ReadFromSreamAsync();
            _client.HandleIncomingMessage(s, _client.ClientStream);
        }

        public bool CanSendLoginData
        {
            get { return (Validator.IsEmail(this.EMail) && Validator.ValidateFields(new[] {this.EMail, this.Password})); }
        }
        #endregion

        #region OpenRegisterDialog
        public void OpenRegisterDialog()
        {
           _windowManager.ShowWindow(new RegisterViewModel());
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
