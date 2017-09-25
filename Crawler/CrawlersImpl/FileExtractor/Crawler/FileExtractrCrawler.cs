using Crawlers.CrawlersImpl.FileExtractor.Crawler.Steps;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.FileExtractor.Crawler
{
    public class FileExtractrCrawler : BaseCrawler
    {
        public FileExtractrCrawler()
        {
            InitializeSteps();
        }

        private void InitializeSteps()
        {
            Steps.Add(1, new FileExtractorFirstStep());
        }
    }
}