using Crawlers.CrawlersImpl.CompanyExtractor.Crawler.Steps;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.CompanyExtractor.Crawler
{
    public class CompanyExtractorCrawler : BaseCrawler
    {
        private CompanyViewModel Company { get; }

        public CompanyExtractorCrawler(CompanyViewModel company)
        {
            Company = company;
            InitializeSteps();
        }

        private void InitializeSteps()
        {
            Steps.Add(1, new CompanyExtractorSearch(Company));
            Steps.Add(2, new CompanyExtractorAddToBasket(Company));
            Steps.Add(3, new CompanyExtractorSaveDetails(Company));
            Steps.Add(4, new CompanyExtractorSendData(Company));
            Steps.Add(5, new CompanyExtractorEcomHandler(Company));
            Steps.Add(6, new CompanyExtractorPostPay());
        }
    }
}