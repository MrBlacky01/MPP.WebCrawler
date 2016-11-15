namespace WebCrawlerWPF.Model
{
    class ClickCountModel : Notifier
    {

        #region Fields

        private int _Count;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set counts of clicks .
        /// </summary>
        public int Count
        {
            get { return _Count; }
            set
            {
                if (_Count != value)
                {
                    _Count = value;
                    OnPropertyChanged("Count");
                }
            }
        }

        #endregion

    }
}
