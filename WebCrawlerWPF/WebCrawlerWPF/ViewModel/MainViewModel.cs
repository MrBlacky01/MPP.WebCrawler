using System.Windows;
using System.Windows.Input;
using WebCrawlerWPF.Model;
using WebCrawlerWPF.ViewModel.Commands;
using WebCrawler;

namespace WebCrawlerWPF.ViewModel
{
    class MainViewModel : Notifier
    {

        #region Fields

        private CrawlResult crawlResult;
        private readonly CrawlerModel crawlerModel;
        private readonly AsyncCommand asyncCommand;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set count of clicks.
        /// </summary>
        public ClickCountModel ClickCount { get; set; }

        /// <summary>
        /// Get or set Result of Crawling
        /// </summary>
        public CrawlResult CrawlResult
        {
            get
            {
                return crawlResult;
            }
            set
            {
                if (crawlResult != value)
                {
                    crawlResult = value;
                    OnPropertyChanged(nameof(CrawlResult));
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Get or set ClickCommand.
        /// </summary>
        public ICommand ClickCountCommand { get; set; }

        /// <summary>
        /// Get or set asyncCommand
        /// </summary>
        public AsyncCommand AsyncCrawlingCommand => asyncCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainViewModel()
        {           
            crawlerModel = new CrawlerModel();
            ClickCount = new ClickCountModel
            {
                Count = 0
            };

            ClickCountCommand = new Command(arg => LeftClickCountMethod());

            asyncCommand = new AsyncCommand(async () =>
            {
                if (asyncCommand.CanExecute(null))
                {
                    CrawlResult = await crawlerModel.GetCrawlerResult();
                }

            });

        }

        #endregion

        #region Methods

        /// <summary>
        /// Left Click method.
        /// </summary>
        private void LeftClickCountMethod()
        {
            ClickCount.Count++;
        }

        #endregion

    }
}
