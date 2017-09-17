using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.Pledges.Crawler.Steps
{
    public class PledgesAddToBasket : ICrawlingStep
    {
        private PledgeViewModel Pledge { get; }

        public PledgesAddToBasket(PledgeViewModel pledge)
        {
            Pledge = pledge;
        }

        public async Task Execute(ICrawlingContext context)
        {
            var payload = BuildPayload(context);
            var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");

            var response = await context.Client.PostAsync("/Search/AddPledgeDetails", content);

            var addToBasketResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            var error = addToBasketResponse["Errors"].Value<JArray>().FirstOrDefault();
            if (error != null)
            {
                throw new Exception(error["ErrorMessage"].Value<string>());
            }
        }

        private JObject BuildPayload(ICrawlingContext context)
        {
            return new JObject
            {
                ["BrowseTollRate"] = "10.00",
                ["ByType"] = "1",
                ["CheckBefore1995"] = "false",
                ["CorporationNameValue"] = context.Get<string>("companyName"),
                ["IdNumRequired"] = Pledge.CompanyId,
                ["OutputType"] = "1",
                ["OwnerType"] = "3"
            };
        }
    }
}