using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebCrawlerWPF.Configuration
{
    internal class XmlConfiguratationSetter : FileConfigurationSetter
    {
        #region Constructor

        public XmlConfiguratationSetter(string fileName):base(fileName)
        {
        }

        #endregion

        #region Methods

        public override CrawlerConfiguration SetConfigurations()
        {
            XDocument xdoc = XDocument.Load(FileName);
            int depth = Int32.Parse( xdoc.Element("root").Element("depth").Value);
            List<string> rootResourses = new List<string>();

            foreach(XElement root in xdoc.Element("rootResources").Elements("resource"))
            {
                rootResourses.Add(root.Value);
            }
            return new CrawlerConfiguration(depth,rootResourses);
        }

        #endregion
    }
}
