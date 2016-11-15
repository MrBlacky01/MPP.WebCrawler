using System.Windows;
using WebCrawlerWPF.View;
using WebCrawlerWPF.ViewModel;

namespace WebCrawlerWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var mw = new MainWindow
            {
                DataContext = new MainViewModel()
            };
            mw.Show();
        }
    }
}
