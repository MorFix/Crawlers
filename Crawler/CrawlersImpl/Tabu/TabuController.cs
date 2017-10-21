using System.Net;
using System.Net.Http;
using System.Reflection;
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
                Content = new StringContent(Assembly.GetExecutingAssembly().GetName().Version.ToString())
            };
        }
    }
}