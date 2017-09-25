using System.Collections.Generic;
using Crawlers.CrawlersImpl.CompanyExtractor.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.CompanyExtractor
{
    public class CompanyExtractorLogic : BaseCrawlerLogic<CompanyExtractorCrawler, CompanyViewModel>
    {
        protected override CompanyExtractorCrawler CreateCrawler(IDictionary<string, string> parameters)
        {
            return new CompanyExtractorCrawler(CreateViewModel(parameters));
        }

        protected override CompanyViewModel CreateViewModel(IDictionary<string, string> parameters)
        {
            var company = base.CreateViewModel(parameters);

            string id;
            parameters.TryGetValue("id", out id);
            company.Id = !string.IsNullOrWhiteSpace(id) ? id : "1";

            return company;
        }
    }
}