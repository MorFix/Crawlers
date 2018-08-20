using System;
using Crawlers.Infra;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Crawlers.CrawlersImpl.Pledges.Crawler.Steps
{
    public class PledgesCompanySearch : ICrawlingStep
    {
        private PledgeViewModel Pledge { get; }

        public PledgesCompanySearch(PledgeViewModel pledge)
        {
            Pledge = pledge;
        }

        public async Task Execute(ICrawlingContext context)
        {
            var companyExistsResponse = await context.Client.GetAsync(
                $"/SearchPledge/GetIsraeliCorporation?companyNumber={Pledge.Id}&owner={(int) Pledge.OwnerType}");

            var jsonResponse = JObject.Parse(await companyExistsResponse.Content.ReadAsStringAsync());
            if (!jsonResponse["valid"].Value<bool>())
            {
                throw new Exception("Company not found");
            }

            context.Set("corporationName", jsonResponse["corporationName"].Value<string>());
        }
    }
}