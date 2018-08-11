using System.Web.Http;
using Crawlers.CrawlersImpl.Tabu.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Tabu
{
    [RoutePrefix("api/tabu")]
    public class TabuController : BaseCrawlerController<TabuLogic, TabuCrawler, NesachViewModel>
    {
        
    }
}