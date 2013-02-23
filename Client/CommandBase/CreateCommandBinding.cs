using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.CommandBase
{
    public static class CreateCommandBinding
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command",
            typeof(CommandModel),
            typeof(CreateCommandBinding),
            new PropertyMetadata(new PropertyChangedCallback(OnCommandInvalidated)));

        public static CommandModel GetCommand(DependencyObject sender)
        {
            return (CommandModel) sender.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject element, CommandModel value)
        {
            element.SetValue(CommandProperty, value);
        }

        // Hier wird das eigentliche Binding des Commands and das UIelement vorgenommen mit den jeweilige Methoden des
        // CommandModels. Das Funktioniert mit fast allen Elementen da DependencyObject eine high-level Klasse ist und in der
        // Klassenherachie ziemmlich weit oben ist.
        private static void OnCommandInvalidated(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)dependencyObject;
            element.CommandBindings.Clear();

            CommandModel commandModel = e.NewValue as CommandModel;
            if (commandModel != null)
            {
                element.CommandBindings.Add(
                    new CommandBinding(commandModel.Command,
                        commandModel.Executed,
                        commandModel.CanExecute));
            }

            CommandManager.InvalidateRequerySuggested();
        }

    }
}
