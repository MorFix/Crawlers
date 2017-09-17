using Crawlers.Infra;
using Crawlers.Pledges.Crawler.Steps;

namespace Crawlers.Pledges.Crawler
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
            Steps.Add(1, new PledgesCompanySearch(Pledge));
            Steps.Add(2, new PledgesAddToBasket(Pledge));
            Steps.Add(3, new PledgesSendData(Pledge));
            Steps.Add(4, new PledgesEncryption());
            Steps.Add(5, new PledgesEcomRedirect());
            Steps.Add(6, new PledgesEcomPay(Pledge));
        }
    }
}