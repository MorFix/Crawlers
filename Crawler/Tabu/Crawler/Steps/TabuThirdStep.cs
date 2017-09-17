using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.Tabu.Crawler.Steps
{
    public class TabuThirdStep : ICrawlingStep
    {
        private NesachViewModel Nesach { get; }

        public TabuThirdStep(NesachViewModel nesach)
        {
            Nesach = nesach;
        }

        public Task Execute(ICrawlingContext context)
        {
            const string url = "/Counter/secureFolder/Payment.aspx?counter=10&catalog=1&category=tabuNesach&paytype=card";

            return CrawlingHelper.PayOnEcom(url, context, Nesach);
        }
    }
}