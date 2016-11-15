using System.Windows;
using System.Windows.Input;
using WebCrawlerWPF.Model;

namespace WebCrawlerWPF.ViewModel
{
    class MainViewModel 
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainViewModel()
        {
            ClickCountCommand = new Command(arg => LeftClickCountMethod());

            ClickCount = new ClickCountModel
            {
                Count = 0
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set count of clicks.
        /// </summary>
        public ClickCountModel ClickCount { get; set; }

        #endregion

       

        #region Commands

        /// <summary>
        /// Get or set ClickCommand.
        /// </summary>
        public ICommand ClickCountCommand { get; set; }

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
