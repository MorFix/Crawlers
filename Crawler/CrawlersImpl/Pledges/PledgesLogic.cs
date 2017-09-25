using System.Collections.Generic;
using Crawlers.CrawlersImpl.Pledges.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Pledges
{
    public class PledgesLogic : BaseCrawlerLogic<PledgesCrawler, PledgeViewModel>
    {
        protected override PledgesCrawler CreateCrawler(IDictionary<string, string> parameters)
        {
            return new PledgesCrawler(CreateViewModel(parameters));
        }

        protected override PledgeViewModel CreateViewModel(IDictionary<string, string> parameters)
        {
            var pledge = base.CreateViewModel(parameters);

            string companyId, output;
            parameters.TryGetValue("companyId", out companyId);
            parameters.TryGetValue("outputType", out output);

            pledge.CompanyId = !string.IsNullOrWhiteSpace(companyId) ? companyId : "1";
            pledge.OutputType = (PledgeOutputType)int.Parse(output ?? "0");

            return pledge;
        }
    }
}