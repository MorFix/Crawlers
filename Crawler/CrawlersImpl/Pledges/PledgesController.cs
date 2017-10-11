using System.Web.Http;
using Crawlers.CrawlersImpl.Pledges.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Pledges
{
    [RoutePrefix("api/pledges")]
    public class PledgesController : BaseCrawlerController<PledgesLogic, PledgesCrawler, PledgeViewModel>
    {
    }
}