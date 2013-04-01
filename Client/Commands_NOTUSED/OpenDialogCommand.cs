using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.CommandBase;
using Client.ViewModels;

namespace Client.Commands
{
    class OpenDialogCommand : CommandModel
    {
        private readonly LoginViewModel _vm;
        //private readonly ViewModelBase.ViewModelBase _m;
        
        public OpenDialogCommand(LoginViewModel vm)
        {
            _vm = vm;
           // _m = modelToOpen;
        }

        public override void Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            //_vm.OpenRegisterView();
        }

        public override void CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            base.CanExecute(sender, e);
        }

    }
}
