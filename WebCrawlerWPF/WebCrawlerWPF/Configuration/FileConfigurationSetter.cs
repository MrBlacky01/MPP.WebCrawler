namespace WebCrawlerWPF.Configuration
{
    internal abstract class FileConfigurationSetter : IConfigurable
    {

        #region Properties

        /// <summary>
        /// Get FileName; 
        /// </summary>
        public string FileName { get; protected set; }

        #endregion

        #region Consructor

        public FileConfigurationSetter(string fileName)
        {
            FileName = fileName;
        }

        #endregion

        #region Methods

        #region abstract

        /// <summary>
        /// Set Configuration for crawling
        /// </summary>
        public abstract CrawlerConfiguration SetConfigurations();

        #endregion

        #endregion

    }
}
