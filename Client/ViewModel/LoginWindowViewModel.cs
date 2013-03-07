using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.CommandBase;
using Client.Commands;
using Client.Helper;
using FowaProtocol.EventArgs;
using FowaProtocol.FowaImplementations;
using FowaProtocol.FowaMessages;

namespace Client.ViewModel
{
    public class LoginWindowViewModel : ViewModelBase.ViewModelBase
    {

        private string _eMail;
        private string _password;
        private readonly CommandModel _sendLogin;
        private readonly FowaClient _client;

        public LoginWindowViewModel()
        {
            _sendLogin = new SendLoginDataCommand(this);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
            _client = new FowaClient();
            _client.Connect(ip);
            _client.IncomingFriendlistMessage += OnIncomingFriendlistMessage;
        }

        public void OnIncomingFriendlistMessage(object sender, IncomingMessageEventArgs e)
        {
            MessageBox.Show("Friendliest");
        }

        public string EMail
        {
            get { return _eMail; }
            set
            {
                if (_eMail == value) return;
                _eMail = value;
                OnPropertyChanged(this, "EMail");
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
            }
        }
        
        public CommandModel SendLoginDataCommand
        {
            get { return _sendLogin; }
        }

        internal async void SendLoginData()
        {
            await _client.WriteToClientStreamAync(new LoginMessage(EMail, Password));
            string s = await _client.ReadFromSreamAsync();
            _client.HandleIncomingMessage(s, _client.ClientStream);
        }

        public override string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;
                switch (columnName)
                {
                    case "EMail":
                        if(!Validator.IsEmail(EMail)) errorMessage = "Invalid e-mail";
                        break;
                    case "Password":
                        break;
                }

                return errorMessage;
            }
        }
    }
}
