using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModelBase
{
    public abstract class ViewModelBase : IDataErrorInfo, INotifyPropertyChanged
    {
        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get { return null; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
