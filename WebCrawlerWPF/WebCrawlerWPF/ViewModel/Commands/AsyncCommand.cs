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

        #endregion

        #region Constructor

        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }

        #endregion

        #region AsyncCommandBase Members

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }

        #endregion
    }
}
