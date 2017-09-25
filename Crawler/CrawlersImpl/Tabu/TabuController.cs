using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Crawlers.CrawlersImpl.Tabu.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Tabu
{
    [RoutePrefix("api/tabu")]
    public class TabuController : BaseCrawlerController<TabuLogic, TabuCrawler, NesachViewModel>
    {
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage Test()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("This is a test")
            };
        }
    }
}