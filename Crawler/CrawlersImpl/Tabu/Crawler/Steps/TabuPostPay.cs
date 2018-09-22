using System;
using System.Linq;
using System.Threading.Tasks;
using Crawlers.Infra;
using HtmlAgilityPack;

namespace Crawlers.CrawlersImpl.Tabu.Crawler.Steps
{
    public class TabuPostPay : ICrawlingStep
    {
        public async Task Execute(ICrawlingContext context)
        {
            var doc = await CrawlingHelper.GetHtmlDocument(context.Client, context.Get<Uri>("downloadPath"));
            var linkToPdf = doc.GetElementbyId("OrderDetailsTB")
                               .Descendants("a")
                               .ToList()[1]
                               .Attributes
                               .FirstOrDefault()?
                               .Value;

            context.Result = $"<a href='{linkToPdf}'>Link to file</a>";
        }
    }
}