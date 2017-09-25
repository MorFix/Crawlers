using System;
using System.Threading.Tasks;
using Crawlers.Infra;
using Crawlers.Infra.Ecom;

namespace Crawlers.CrawlersImpl.Tabu.Crawler.Steps
{
    public class TabuPayment : ICrawlingStep
    {
        private NesachViewModel Nesach { get; }

        public TabuPayment(NesachViewModel nesach)
        {
            Nesach = nesach;
        }

        public async Task Execute(ICrawlingContext context)
        {
            const string url = "/Counter/secureFolder/Payment.aspx?counter=10&catalog=1&category=tabuNesach&paytype=card";

            await new EcomPayer(context, new Uri(url, UriKind.Relative), Nesach).Pay();
        }
    }
}