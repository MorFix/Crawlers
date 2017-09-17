using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.Pledges.Crawler.Steps
{
    public class PledgesEcomPay : ICrawlingStep
    {
        public PledgeViewModel Pledge { get; }

        public PledgesEcomPay(PledgeViewModel pledge)
        {
            Pledge = pledge;
        }

        public Task Execute(ICrawlingContext context)
        {
            const string url =
                "/Counter/secureFolder/Payment.aspx?counter=53&catalog=1&category=JusticePayments_1_BrowseOnlinePayment&language=he&paytype=card";

            return CrawlingHelper.PayOnEcom(url, context, Pledge);
        }
    }
}