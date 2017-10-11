using System;
using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Pledges.Crawler.Steps
{
    public class PledgesPostPay : ICrawlingStep
    {
        public Task Execute(ICrawlingContext context)
        {
            var path = context.Get<Uri>("downloadPath");
            context.Result = $"<a href='{path}'>{path}</a>";

            return Task.FromResult(true);
        }
    }
}