using System.Collections.Generic;
using System.Threading.Tasks;
using Crawlers.Tabu.Crawler;

namespace Crawlers.Tabu
{
    public class TabuLogic
    {
        public async Task PurchaseAsync(IDictionary<string, string> queryString)
        {
            string gush, helka, subHelka, type, name, email;
            queryString.TryGetValue("gush", out gush);
            queryString.TryGetValue("helka", out helka);
            queryString.TryGetValue("subHelka", out subHelka);
            queryString.TryGetValue("type", out type);
            queryString.TryGetValue("name", out name);
            queryString.TryGetValue("email", out email);


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