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
            context.Client.BaseAddress = new Uri("https://pledges.justice.gov.il");
            context.HttpHandler.AllowAutoRedirect = false;

            var companyExistsResponse = await context.Client.GetAsync(string.Format("/Search/GetIsraeliCorporation?companyNumber={0}&owner=3", Pledge.CompanyId));

            var jsonResponse = JObject.Parse(await companyExistsResponse.Content.ReadAsStringAsync());
            if (!jsonResponse["valid"].Value<bool>())
            {
                throw new Exception("Company not found");
            }

            context.Set("companyName", jsonResponse["corporationName"].Value<string>());
        }
    }
}