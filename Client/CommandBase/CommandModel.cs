using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.CommandBase
{
    public abstract class CommandModel
    {
        private RoutedCommand _routedCommand;

        protected CommandModel()
        {
            _routedCommand = new RoutedCommand();
        }

        public RoutedCommand Command
        {
            get { return _routedCommand; }
        }

        public virtual void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        public abstract void Executed(object sender, ExecutedRoutedEventArgs e);
    }
}
