using System.Collections.Generic;

namespace WebCrawler
{
    public sealed class CrawlResult
    {
        #region Properties

        /// <summary>
        /// Get or set dictionary of CrawResults
        /// </summary>
        public Dictionary <string, CrawlResult> Urls { get; set; }

        #endregion
    }
}
