using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.CommandBase;
using Client.Commands;
using Client.Helper;

namespace Client.ViewModel
{
    public class LoginWindowViewModel : ViewModelBase.ViewModelBase
    {

        private string _eMail;
        private string _password;
        private readonly CommandModel _sendLogin;

        public LoginWindowViewModel()
        {
            _sendLogin = new SendLoginDataCommand(this);
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

        internal void SendLoginData()
        {
            MessageBox.Show("Sende Logindaten");
        }


    }
}
