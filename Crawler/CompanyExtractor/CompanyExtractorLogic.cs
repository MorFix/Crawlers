using System.Collections.Generic;
using System.Threading.Tasks;
using Crawlers.CompanyExtractor.Crawler;

namespace Crawlers.CompanyExtractor
{
    public class CompanyExtractorLogic
    {
        public async Task PurchaseAsync(IDictionary<string, string> queryString)
        {
            string id;
            queryString.TryGetValue("id", out id);

            var company = new CompanyViewModel
            {
                Id = !string.IsNullOrWhiteSpace(id) ? id : null,
            };

            await new CompanyExtractorCrawler(company).Crawl();
        }
    }
}