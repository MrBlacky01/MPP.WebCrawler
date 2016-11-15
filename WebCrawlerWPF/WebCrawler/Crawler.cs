using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Crawler : ISimpleWebCrawler
    {
        #region Properties

        /// <summary>
        /// Get or set depth of crawling.
        /// </summary>
        private int CrawlingDepth
        {
            get { return CrawlingDepth; }
            set
            {
                if(CheckRightDepth(value))
                {
                    CrawlingDepth = 6;
                }
                else
                {
                    CrawlingDepth = value;
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Crawler Constructor.
        /// </summary>
        Crawler(int _depth, string[] _urls)
        {

        }
        #endregion

        #region Methods
    
        /// <summary>
        /// Check is the depth right
        /// </summary>
        /// 
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
