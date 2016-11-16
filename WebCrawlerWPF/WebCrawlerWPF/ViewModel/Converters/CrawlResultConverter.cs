using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawler;

namespace WebCrawlerWPF.ViewModel.Converters
{
    internal class CrawlResultConverter: IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Method to convert CrawlResult To TreeViewItem
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CrawlResult crawlResult = value as CrawlResult;
            if (crawlResult == null)
            {
                return new object();
            }

            TreeViewItem currentTreeViewItem = CreateTreeViewItem(string.Empty);
            ConvertCrawlResultToTreeViewItem(crawlResult, currentTreeViewItem);
            
            return new List<TreeViewItem>() { currentTreeViewItem } ;
        }

        /// <summary>
        /// Method to convert TreeViewItem to CrawlResult(don't use), only throw NonImplementedException
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create TreeView structure from CrawlResult
        /// </summary>
        private TreeViewItem ConvertCrawlResultToTreeViewItem(CrawlResult crawlResult, TreeViewItem treeViewItem)
        {
            if (crawlResult == null)
            {
                return null;
            }
            foreach (KeyValuePair<string, CrawlResult> urlUnit in crawlResult.Urls)
            {
                TreeViewItem currentTreeViewItem = CreateTreeViewItem(urlUnit.Key);
                ConvertCrawlResultToTreeViewItem(urlUnit.Value, currentTreeViewItem);
                treeViewItem.Items.Add(currentTreeViewItem);
            }
            return treeViewItem;
        }

        /// <summary>
        /// Create TreeViewItem according to name
        /// </summary>
        private TreeViewItem CreateTreeViewItem(string header)
        {
            return new TreeViewItem() { Header = header };
        }

        #endregion
    }
}
