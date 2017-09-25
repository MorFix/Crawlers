using Crawlers.Infra.Ecom;

namespace Crawlers.CrawlersImpl.Pledges
{
    public class PledgeViewModel : BaseViewModel
    {
        public string CompanyId { get; set; }
        public PledgeOutputType OutputType { get; set; }
    }
}