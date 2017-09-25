using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorEcomPay : ICrawlingStep
    {
        public CompanyViewModel Company { get; }

        public CompanyExtractorEcomPay(CompanyViewModel company)
        {
            Company = company;
        }

        public Task Execute(ICrawlingContext context)
        {
            const string url =
                "/Counter/secureFolder/Payment.aspx?counter=53&catalog=1&category=JusticePayments_1_CompanyRegistration&language=he&paytype=card";

            return CrawlingHelper.PayOnEcom(url, context, Company);
        }
    }
}