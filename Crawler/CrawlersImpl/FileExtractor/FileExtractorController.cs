using System.Web.Http;
using Crawlers.CrawlersImpl.FileExtractor.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.FileExtractor
{
    [RoutePrefix("api/fileExtractor")]
    public class FileExtractorController : BaseCrawlerController<FileExtractorLogic, FileExtractrCrawler, FileExtractorViewModel>
    {
    }
}