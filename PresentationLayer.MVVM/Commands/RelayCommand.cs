using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.MVVM.Commands
{
    
    public class RelayCommand : CommandBase
    {
        private readonly Action _execute = null!;
        private readonly Func<bool> _canExecute = null!;
        public RelayCommand() { }
        public RelayCommand(Action execute) : this(execute, null!) { }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public override void Execute(object parameter) { _execute(); }
        public override bool CanExecute(object parameter) =>
        _canExecute == null || _canExecute();
    }
    public class RelayCommand<T> : RelayCommand
    {
        private readonly Action<T> _execute = null!;
        private readonly Func<T, bool> _canExecute = null!;
        public RelayCommand(Action<T> execute) : this(execute, null!) { }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public override void Execute(object parameter) { _execute((T)parameter); }
        public override bool CanExecute(object parameter) =>
        _canExecute == null || _canExecute((T)parameter);
    }

    public class RelayCommandNewVersion : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommandNewVersion(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
