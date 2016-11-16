using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerWPF.ViewModel.Commands
{
    interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
