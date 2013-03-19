using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.CommandBase;
using Client.Helper;
using Client.ViewModels;

namespace Client.Commands
{
    public class SendLoginDataCommand : CommandModel
    {
        private readonly LoginViewModel _viewModel;

        public SendLoginDataCommand(LoginViewModel viewModel)
        {
            this._viewModel = viewModel;
        }
        
        public override void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Validator.IsEmail(_viewModel.EMail) && Validator.ValidateFields(new[] { _viewModel.EMail, _viewModel.Password }));
        }

        public override void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.SendLoginData();
        }
    }
}
