using System.Web.Http;
using Crawlers.CrawlersImpl.CompanyExtractor.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.CompanyExtractor
{
    [RoutePrefix("api/companyExtractor")]
    public class CompanyExtractorController : BaseCrawlerController<CompanyExtractorLogic, CompanyExtractorCrawler, CompanyViewModel>
    {
    }
}