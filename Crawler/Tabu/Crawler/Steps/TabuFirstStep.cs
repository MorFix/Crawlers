using System;
using System.Threading.Tasks;
using Crawlers.Infra;
using HtmlAgilityPack;

namespace Crawlers.Tabu.Crawler.Steps
{
    public class TabuFirstStep : ICrawlingStep
    {
        private const string Url = "/Counter/alternative/tabuNesach/Homepage.aspx?counter=10";

        public async Task Execute(ICrawlingContext context)
        {
            context.Client.BaseAddress = new Uri("https://ecom.gov.il");
            context.HttpHandler.AllowAutoRedirect = false;

            var response = await context.Client.GetAsync(Url);

            var html = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            CrawlingHelper.SetEventParams(context, doc);
        }
    }
}