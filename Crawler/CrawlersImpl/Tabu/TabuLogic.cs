using System.Collections.Generic;
using Crawlers.CrawlersImpl.Tabu.Crawler;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Tabu
{
    public class TabuLogic : BaseCrawlerLogic<TabuCrawler, NesachViewModel>
    {
        protected override TabuCrawler CreateCrawler(IDictionary<string, string> parameters)
        {
            return new TabuCrawler(CreateViewModel(parameters));
        }

        protected override NesachViewModel CreateViewModel(IDictionary<string, string> parameters)
        {
            var nesach = base.CreateViewModel(parameters);

            string gush, helka, subHelka, type;
            parameters.TryGetValue("gush", out gush);
            parameters.TryGetValue("helka", out helka);
            parameters.TryGetValue("subHelka", out subHelka);
            parameters.TryGetValue("type", out type);

            nesach.Gush = gush;
            nesach.Helka = helka;
            nesach.SubHelka = subHelka;
            nesach.Type = (NesachType) int.Parse(type ?? "0");

            return nesach;
        }
    }
}