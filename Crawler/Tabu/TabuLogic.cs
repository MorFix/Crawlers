using System.Collections.Generic;
using System.Threading.Tasks;
using Crawlers.Tabu.Crawler;

namespace Crawlers.Tabu
{
    public class TabuLogic
    {
        public async Task PurchaseAsync(IDictionary<string, string> queryString)
        {
            queryString.TryGetValue("gush", out var gush);
            queryString.TryGetValue("helka", out var helka);
            queryString.TryGetValue("subHelka", out var subHelka);
            queryString.TryGetValue("type", out var type);
            queryString.TryGetValue("name", out var name);
            queryString.TryGetValue("email", out var email);


            var nesach = new NesachViewModel
            {
                Gush = gush,
                Helka = helka,
                SubHelka = subHelka,
                Type = (NesachType) int.Parse(type ?? "0"),
                Name = !string.IsNullOrWhiteSpace(name) ? name : null,
                Email = !string.IsNullOrWhiteSpace(email) ? email : null,
            };

            await new TabuCrawler(nesach).Crawl();
        }
    }
}