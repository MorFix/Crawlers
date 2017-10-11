using System.Collections.Generic;
using System.Configuration;
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
            parameters.TryGetValue("name", out var name);
            parameters.TryGetValue("email", out var email);

            return new TViewModel
            {
                Name = !string.IsNullOrWhiteSpace(name) ? name : ConfigurationManager.AppSettings["name"],
                Email = !string.IsNullOrWhiteSpace(email) ? email : ConfigurationManager.AppSettings["email"]
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