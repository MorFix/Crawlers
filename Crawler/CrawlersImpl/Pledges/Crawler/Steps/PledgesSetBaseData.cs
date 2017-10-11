using System;
using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Pledges.Crawler.Steps
{
    public class PledgesSetBaseData : ICrawlingStep
    {
        public Task Execute(ICrawlingContext context)
        {
            context.Client.BaseAddress = new Uri("https://pledges.justice.gov.il");
            context.HttpHandler.AllowAutoRedirect = false;

            return Task.FromResult(true);
        }
    }
}