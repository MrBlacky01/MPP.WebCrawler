using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class Crawler : ISimpleWebCrawler, IDisposable
    {
        #region Fields

        /// <summary>
        /// Field that indicate of calling Dispose method
        /// </summary>
        private bool isDisposed;

        private int crawlingDepth;
        #endregion
         
        #region Properties

        /// <summary>
        /// Get or set depth of crawling.
        /// </summary>
        public int CrawlingDepth
        {
            get { return crawlingDepth; }
            set
            {
                if (CheckRightDepth(value))
                {
                    crawlingDepth = value;
                }
                else
                {
                    crawlingDepth = 6;
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
        public Crawler(int _depth)
        {
            CrawlingDepth = _depth;
            Client = new HttpClient();
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

            Dictionary<string, CrawlResult> result = new Dictionary<string, CrawlResult>();
            Task<CrawlResult>[] childs = new Task<CrawlResult>[_rootUrls.Length];

            for(int i = 0; i < _rootUrls.Length; i++)
            {
                childs[i] = GetCrawresultFromUrl(_rootUrls[i], 1);
            }

            await Task.WhenAll(childs);

            for(int i = 0; i < _rootUrls.Length; i++)
            {
                if (!result.ContainsKey(_rootUrls[i]))
                {
                    result.Add(_rootUrls[i], childs[i].Result);
                }
            }

            return new CrawlResult { Urls = result };
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

        #region Private

        /// <summary>
        /// Get CrawResult according url and depth
        /// </summary>
        private async Task<CrawlResult> GetCrawresultFromUrl(string url, int currentDepth)
        {
            Dictionary<string,CrawlResult> result = new Dictionary<string, CrawlResult>();
            if(currentDepth <= CrawlingDepth)
            {
                string htmlDocument = await LoadHtml(url);
                if (htmlDocument == null)
                {
                    return new CrawlResult();
                }

                List<string> urls = new List<string>( FindLinksInHtmlDocument(htmlDocument));

                Task<CrawlResult>[] childs = new Task<CrawlResult>[urls.Count];

                for (int i = 0; i < urls.Count; i++)
                {
                    childs[i] = GetCrawresultFromUrl(urls[i], currentDepth + 1);
                }

                await Task.WhenAll(childs);

                for (int i = 0; i < urls.Count; i++)
                {
                    if (!result.ContainsKey(urls[i]))
                    {
                        result.Add(urls[i], childs[i].Result);
                    }
                }
                
            }
            return new CrawlResult { Urls = result};
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
        private ConcurrentBag<string> FindLinksInHtmlDocument(string html)
        {
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                ConcurrentBag<string> hrefList = new ConcurrentBag<string>();

                Parallel.ForEach(htmlDocument.DocumentNode.Descendants("a"), (link) =>
                {
                    if (link.Attributes.Contains("href"))
                    {
                        string attribute = link.Attributes["href"].Value;
                        if (attribute.StartsWith("http"))
                        {
                            hrefList.Add(attribute);
                        }

                    }
                });
                return hrefList;
            }
            catch(Exception e)
            {
                return null;
            }
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

        #endregion

    }
}
