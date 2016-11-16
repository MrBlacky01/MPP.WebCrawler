using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class Crawler : ISimpleWebCrawler
    {
        #region Fields

        /// <summary>
        /// Field that indicate of calling Dispose method
        /// </summary>
        private bool isDisposed;

        #endregion
         
        #region Properties

        /// <summary>
        /// Get or set depth of crawling.
        /// </summary>
        private int CrawlingDepth
        {
            get { return CrawlingDepth; }
            set
            {
                if (CheckRightDepth(value))
                {
                    CrawlingDepth = 6;
                }
                else
                {
                    CrawlingDepth = value;
                }
            }
        }

        /// <summary>
        /// Get or set HttpClient
        /// </summary>
        private HttpClient Client { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Crawler Constructor.
        /// </summary>
        Crawler(int _depth)
        {
            CrawlingDepth = _depth;

        }
        #endregion

        #region Methods

        #region public 

        /// <summary>
        /// Async method that return CralwResult
        /// </summary>
        public async Task<CrawlResult> PerformCrawlingAsync(string[] _rootUrls)
        {
            if (isDisposed == true)
            {
                throw new ObjectDisposedException(nameof(Crawler));
            }

            if (_rootUrls.Length == 0)
            {
                return new CrawlResult();
            }

            CrawlResult result = new CrawlResult();
            Task<CrawlResult>[] childs = new Task<CrawlResult>[_rootUrls.Length];

            for(int i = 0; i < _rootUrls.Length; i++)
            {
                childs[i] = GetCrawresultFromUrl(_rootUrls[i], 1);
            }

            await Task.WhenAll(childs);

            for(int i = 0; i < _rootUrls.Length; i++)
            {
                result.Urls.Add(_rootUrls[i], childs[i].Result);
            }

            return result;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                Client.Dispose();
                isDisposed = true;
            }

        }

        #endregion

        /// <summary>
        /// Get CrawResult according url and depth
        /// </summary>
        private async Task<CrawlResult> GetCrawresultFromUrl(string url, int currentDepth)
        {
            CrawlResult result = new CrawlResult();
            if(currentDepth <= CrawlingDepth)
            {
                string htmlDocument = await LoadHtml(url);
                if (htmlDocument == null)
                {
                    return result;
                }

                List<string> urls = FindLinksInHtmlDocument(htmlDocument);

                Task<CrawlResult>[] childs = new Task<CrawlResult>[urls.Count];

                for (int i = 0; i < urls.Count; i++)
                {
                    childs[i] = GetCrawresultFromUrl(urls[i], currentDepth + 1);
                }

                await Task.WhenAll(childs);

                for (int i = 0; i < urls.Count; i++)
                {
                    result.Urls.Add(urls[i], childs[i].Result);
                }
            }
            return new CrawlResult();
        }


        /// <summary>
        /// Load html of page according url
        /// </summary>
        private async Task<string> LoadHtml(string url)
        {
            try
            {
                return await Client.GetStringAsync(url);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Find all href tags in html document
        /// </summary>
        private List<string> FindLinksInHtmlDocument(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            List<string> hrefList = new List<string>();
            foreach (var tagA in htmlDocument.DocumentNode.Descendants("a"))
            {
                if (tagA.Attributes.Contains("htef"))
                {
                    string attribute = tagA.Attributes["href"].Value;
                    if (attribute.StartsWith("http"))
                    {
                        hrefList.Add(attribute);
                    }

                }
            }
            return hrefList;

        }

        /// <summary>
        /// Check is the depth right
        /// </summary>
        private bool CheckRightDepth(int depth)
        {
            if((depth < 1) ||(depth > 6))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

    }
}
