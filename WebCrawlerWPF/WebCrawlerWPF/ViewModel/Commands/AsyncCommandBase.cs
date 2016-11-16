using System;
using System.Windows.Input;
using System.Threading.Tasks;

namespace WebCrawlerWPF.ViewModel.Commands
{
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        #region IAsyncCommand Members

        public abstract bool CanExecute(object parameter);
        public abstract Task ExecuteAsync(object parameter);

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region AsyncCommandBase Methods

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion
    }
}
