using Crawlers.CrawlersImpl.Tabu.Crawler.Steps;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Tabu.Crawler
{
    public class TabuCrawler : BaseCrawler
    {
        private NesachViewModel Nesach { get; }

        public TabuCrawler(NesachViewModel nesach)
        {
            Nesach = nesach;
            InitializeSteps();
        }

        private void InitializeSteps()
        {
            Steps.Add(1, new TabuSetEventParams());
            Steps.Add(2, new TabuSendData(Nesach));
            Steps.Add(3, new TabuPayment(Nesach));
        }
    }
}