using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Helper;

namespace Client.ViewModels
{
    public class RegisterViewModel : ViewModelBase.ViewModelBase
    {

        #region Fields

        private string _nickName;
        private string _eMail;
        private string _password;
        private string _confirmedPassword;
        private string _info;

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

        public RegisterViewModel()
        {
            Info = "Register info\n\tFill in the fields and register to be cool :D\n\t...";
            Password = "hans";
        }

        #endregion



        #region SendRegisterMessage

        public void SendRegisterMessage()
        {
            Info = "hans";
            MessageBox.Show(NickName + "\n" + Password + "\n" + ConfirmedPassword + "\n" + Email + "\n" + Info);
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

    }
}
