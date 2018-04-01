using System.Net;
using Crawlers.Infra;
using Crawlers.CrawlersImpl.Pledges.Crawler.Steps;
using Crawlers.CrawlersImpl.Pledges.Enums;

namespace Crawlers.CrawlersImpl.Pledges.Crawler
{
    public class PledgesCrawler : BaseCrawler
    {
        private PledgeViewModel Pledge { get; }

        public PledgesCrawler(PledgeViewModel pledge)
        {
            Pledge = pledge;
            InitializeSteps();
        }

        private void InitializeSteps()
        {
            Steps.Add(1, new PledgesSetBaseData());

            if (Pledge.ViewType == PledgeViewType.ByOwner && PledgesHelper.IsSearchableOwnerType(Pledge.OwnerType))
            {
                Steps.Add(2, new PledgesCompanySearch(Pledge));
            }

            Steps.Add(3, new PledgesAddToBasket(Pledge));
            Steps.Add(4, new PledgesSendData(Pledge));
            Steps.Add(5, new PledgesEcomHandler(Pledge));
            Steps.Add(6, new PledgesPostPay());
        }
    }
}