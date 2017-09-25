using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Crawlers.Infra.Ecom;

namespace Crawlers.Infra
{
    public abstract class BaseCrawlerController<TLogic, TCrawler, TViewModel> : ApiController
        where TLogic : BaseCrawlerLogic<TCrawler, TViewModel>, new ()
        where TViewModel : BaseViewModel, new()
        where TCrawler : BaseCrawler
    {
        protected TLogic Logic { get; }

        protected BaseCrawlerController()
        {
            Logic = new TLogic();
        }

        [Route]
        [HttpGet]
        public async Task<HttpResponseMessage> CrawlAsync()
        {
            var result = await Logic.CrawlAsync(CrawlingHelper.GetQueryString(Request));

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result.ToString())
            };
        }
    }
}