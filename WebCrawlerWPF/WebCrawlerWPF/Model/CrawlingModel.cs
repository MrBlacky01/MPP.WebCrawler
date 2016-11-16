using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;
using WebCrawlerWPF.Configuration;

namespace WebCrawlerWPF.Model
{
    internal class CrawlerModel
    {
        #region Fields

        private readonly string configurationFile = "CrawlerConfig.xml";
        private readonly FileConfigurationSetter configurator;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set configurations
        /// </summary>
        private CrawlerConfiguration config { get; set; }

        #endregion

        #region Constructor 

        public CrawlerModel()
        {
            configurator = new XmlConfiguratationSetter(configurationFile);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Async get result of crawling
        /// </summary>
        public async Task<CrawlResult> GetCrawlerResult()
        {
            config = configurator.SetConfigurations();
            
            using (Crawler crawler = new Crawler(config.Depth))
            {
                return await crawler.PerformCrawlingAsync(config.UrlList.ToArray());
            }
        }   

        #endregion

    }
}
