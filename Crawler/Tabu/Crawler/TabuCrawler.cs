using Crawlers.Infra;
using Crawlers.Tabu.Crawler.Steps;

namespace Crawlers.Tabu.Crawler
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
            Steps.Add(1, new TabuFirstStep());
            Steps.Add(2, new TabuSecondStep(Nesach));
            Steps.Add(3, new TabuThirdStep(Nesach));
        }
    }
}