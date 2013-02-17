using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _vorname;
        private string _nachname;

        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (!value.Equals(Vorname))
                {
                    _vorname = value;
                    OnPropertyChanged("Vorname");
                }
            }
        }

        public string Nachname
        {
            get { return _nachname; }
            set
            {
                if (!value.Equals(Nachname))
                {
                    _nachname = value;
                    OnPropertyChanged("Nachname");
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
