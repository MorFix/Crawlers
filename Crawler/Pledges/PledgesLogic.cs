using System.Collections.Generic;
using System.Threading.Tasks;
using Crawlers.Pledges.Crawler;

namespace Crawlers.Pledges
{
    public class PledgesLogic
    {
        public async Task PurchaseAsync(IDictionary<string, string> queryString)
        {
            string companyId, output, name, email;
            queryString.TryGetValue("companyId", out companyId);
            queryString.TryGetValue("outputType", out output);
            queryString.TryGetValue("name", out name);
            queryString.TryGetValue("email", out email);

            var pledge = new PledgeViewModel
            {
                CompanyId = !string.IsNullOrWhiteSpace(companyId) ? companyId : "1",
                Email = !string.IsNullOrWhiteSpace(email) ? email : null,
                Name = !string.IsNullOrWhiteSpace(name) ? name : null,
                OutputType = (PledgeOutputType) int.Parse(output ?? "0")
            };

            await new PledgesCrawler(pledge).Crawl();
        }
    }
}