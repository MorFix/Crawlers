using System.Collections.Generic;
using Crawlers.CrawlersImpl.FileExtractor.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.FileExtractor
{
    public class FileExtractorLogic : BaseCrawlerLogic<FileExtractrCrawler, FileExtractorViewModel>
    {
        protected override FileExtractrCrawler CreateCrawler(IDictionary<string, string> parameters)
        {
            return new FileExtractrCrawler();
        }
    }
}