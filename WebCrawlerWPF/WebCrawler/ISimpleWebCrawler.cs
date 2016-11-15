using System.Threading.Tasks;

namespace WebCrawler
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls);
    }
}
