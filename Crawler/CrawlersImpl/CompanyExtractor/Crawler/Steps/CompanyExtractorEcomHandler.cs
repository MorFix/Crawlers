using System;
using Crawlers.Infra;
using Crawlers.Infra.Ecom;

namespace Crawlers.CrawlersImpl.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorEcomHandler : EcomHandler
    {
        protected override Uri EcomTarget { get; }
        protected override Uri PaymentUrl { get; }

        public CompanyExtractorEcomHandler(BaseViewModel viewModel) : base(viewModel)
        {
            EcomTarget = new Uri("/Counter/general/Direction.aspx?counter=53&catalog=1&category=JusticePayments_1_CompanyRegistration&language=he", UriKind.Relative);
            PaymentUrl = new Uri("/Counter/secureFolder/Payment.aspx?counter=53&catalog=1&category=JusticePayments_1_CompanyRegistration&language=he&paytype=card", UriKind.Relative);
        }

        protected override Uri GetConnectionEndpoint(ICrawlingContext context)
        {
            return new Uri("/Toll/Shoam/?documentsProduction=true&requestId=" + context.Get<string>("requestId"), UriKind.Relative);
        }
    }
}