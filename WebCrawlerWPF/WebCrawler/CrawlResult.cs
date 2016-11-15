using System.Collections.Concurrent;

namespace WebCrawler
{
    public sealed class CrawlResult
    {
        #region Properties

        /// <summary>
        /// Get or set dictionary of CrawResults
        /// </summary>
        public ConcurrentDictionary <string, CrawlResult> Urls { get; set; }

        #endregion
    }
}
