using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerWPF.ViewModel.Commands
{
    class AsyncCommand : AsyncCommandBase
    {
        #region Fields

        private readonly Func<Task> _command;
        private Func<object, bool> canExecute;

        #endregion

        #region Constructor

        public AsyncCommand(Func<Task> command, Func<object, bool> _canExecute = null)
        {
            _command = command;
            canExecute = _canExecute;
        }

        #endregion

        #region AsyncCommandBase Members

        public override bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }

        #endregion
    }
}
