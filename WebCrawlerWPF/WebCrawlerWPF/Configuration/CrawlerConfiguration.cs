using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.Configuration
{
    internal class CrawlerConfiguration
    {
        #region Consructor

        public CrawlerConfiguration(int _depth, List<String> _urlList)
        {
            Depth = _depth;
            UrlList = _urlList;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get Depth; set - protected;
        /// </summary>
        public int Depth { get; protected set; }

        /// <summary>
        /// Get List of urls; set- protected
        /// </summary>
        public List<String> UrlList { get; protected set; }

        #endregion
  
    }
}
