using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FluentAdb.Sample
{
    public class AsyncCommand<T> : ICommand
    {
        private readonly Func<T, Task> _action;
        private readonly CancellationToken _cancellationToken;

        public AsyncCommand(Func<T, Task> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            _action = action;
            _cancellationToken = cancellationToken;
        }

        public bool CanExecute(object parameter)
        {
            return !_cancellationToken.IsCancellationRequested;
        }

        public async void Execute(object parameter)
        {
            if (parameter == null || parameter is T)
                await _action((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class AsyncCommand : AsyncCommand<object>
    {
        public AsyncCommand(Func<Task> action, CancellationToken cancellationToken)
            : base(async _ => await action(), cancellationToken)
        {
        }
    }
}
