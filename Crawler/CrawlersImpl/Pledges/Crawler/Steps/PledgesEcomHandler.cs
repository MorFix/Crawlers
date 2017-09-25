using System;
using Crawlers.Infra;
using Crawlers.Infra.Ecom;

namespace Crawlers.CrawlersImpl.Pledges.Crawler.Steps
{
    public class PledgesEcomHandler : EcomHandler
    {

        protected override Uri EcomTarget { get; }
        protected override Uri PaymentUrl { get; }

        public PledgesEcomHandler(BaseViewModel viewModel) : base(viewModel)
        {
            EcomTarget = new Uri("/Counter/general/Direction.aspx?counter=53&catalog=1&category=JusticePayments_1_BrowseOnlinePayment&language=he", UriKind.Relative);
            PaymentUrl = new Uri("/Counter/secureFolder/Payment.aspx?counter=53&catalog=1&category=JusticePayments_1_BrowseOnlinePayment&language=he&paytype=card", UriKind.Relative);
        }

        protected override Uri GetConnectionEndpoint(ICrawlingContext context)
        {
            return new Uri("/General/Shoam/?guidKey=" + context.Get<string>("guidKey"), UriKind.Relative);
        }
    }
}