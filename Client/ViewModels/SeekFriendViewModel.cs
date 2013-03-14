using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class SeekFriendViewModel : ViewModelBase.ViewModelBase
    {

        #region Fields

        private string _email;
        private string _nickName;
        private int _uid;

        #endregion

        #region Properties

        public string Email
        {
            get { return _email; }
            set
            {
                if(_email == value) return;
                _email = value;
                OnPropertyChanged(this, "Email");
            }
        }

        public string NickName
        {
            get { return _nickName; }
            set
            {
                if(_nickName == value) return;
                _nickName = value;
                OnPropertyChanged(this, "NickName");
            }
        }
        public int Uid
        {
            get { return _uid; }
            set
            {
                if(_uid == value) return;
                _uid = value;
                OnPropertyChanged(this, "Uid");
            }
        }

        #endregion

       
    }
}
