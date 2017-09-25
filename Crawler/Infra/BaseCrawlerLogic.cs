using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra.Ecom;

namespace Crawlers.Infra
{
    public abstract class BaseCrawlerLogic<TCrawler, TViewModel> 
        where TCrawler : BaseCrawler
        where TViewModel : BaseViewModel, new()
    {
        protected abstract TCrawler CreateCrawler(IDictionary<string, string> parameters);

        protected virtual TViewModel CreateViewModel(IDictionary<string, string> parameters)
        {
            string name, email;
            parameters.TryGetValue("name", out name);
            parameters.TryGetValue("email", out email);

            return new TViewModel
            {
                Name = !string.IsNullOrWhiteSpace(name) ? name : null,
                Email = !string.IsNullOrWhiteSpace(email) ? email : null
            };
        }

        public async Task<object> CrawlAsync(IDictionary<string, string> queryString)
        {
            var crawler = CreateCrawler(queryString);
            await crawler.Crawl();

            return crawler.Context.Result.ToString();
        }
    }
}