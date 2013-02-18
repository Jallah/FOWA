using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol
{

    // Just a test class
    public class Contact : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _nickName;
        private int _userId;

        public string NickName
        {
            get { return _nickName; }
            set
            {
                if (!value.Equals(NickName))
                {
                    _nickName = value;
                    OnPropertyChanged("NickName");
                }
            }
        }

        public int UserId
        {
            get { return _userId; }
            set
            {
                if (!value.Equals(UserId))
                {
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public void OnPropertyChanged(string propName)
        {
            var eh = PropertyChanged;
            if (eh != null)
            {
                eh(this, new PropertyChangedEventArgs(propName));
            }
        }


        private string straße;

        public string getStraße()
        {
            return straße;
        }

        public void setStraße(string straße)
        {
            this.straße = straße;
        }

        public string Straße { get; private set; }

    }
}
