using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.CommandBase;
using Client.Helper;
using Client.ViewModel;

namespace Client.Commands
{
    public class SendLoginDataCommand : CommandModel
    {
        private readonly LoginWindowViewModel _viewModel;

        public SendLoginDataCommand(LoginWindowViewModel viewModel)
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
