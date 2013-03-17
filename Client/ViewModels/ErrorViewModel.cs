using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ErrorViewModel : ViewModelBase.ViewModelBase
    {
        public ErrorViewModel(string errorMessge)
        {
            _errorMessage = errorMessge;
        }

        public void Button()
        {
            ErrorMessage = "test";
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if(_errorMessage == null) return;
                _errorMessage = value;
                OnPropertyChanged(this, "ErrorMessage");
            }
        }
    }
}
