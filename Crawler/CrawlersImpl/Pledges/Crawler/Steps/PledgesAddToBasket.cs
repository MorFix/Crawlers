using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Crawlers.CrawlersImpl.Pledges.Enums;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.CrawlersImpl.Pledges.Crawler.Steps
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

            context.Set("responseModel", addToBasketResponse["Model"].Value<JObject>());
        }

        private JObject BuildPayload(ICrawlingContext context)
        {
            return new JObject
            {
                ["Asset"] = (int) Pledge.AssetType,
                ["BankId"] = Pledge.BankNumber,
                ["BankId_input"] = Pledge.BankNumber,
                ["BankName"] = Pledge.BankNumber,
                ["BankNameString"] = "",
                ["BankName_input"] = "",
                ["BrowseTollRate"] = "10.00",
                ["ByType"] = (int) Pledge.ViewType,
                ["CheckBefore1995"] = "false",
                ["CheckBefore1995Id"] = "",
                ["CheckBefore1995Id_input"] = "",
                ["CorporationName"] = "",
                ["CorporationNameValue"] = context.Get<string>("corporationName"),
                ["CountryId"] = "",
                ["CountryId_input"] = "",
                ["CountryName"] = "",
                ["FirstName"] = "",
                ["FirstNameEnglish"] = "",
                ["FirstNameForeign"] = "",
                ["ForeignCorporationName"] = "",
                ["IdForeignResident"] = "",
                ["IdNum"] = "",
                ["IdNumRequired"] = Pledge.Id,
                ["LastName"] = "",
                ["LastNameEnglish"] = "",
                ["LastNameForeign"] = "",
                ["LicenseNum"] = Pledge.LicenseNumber,
                ["OutputType"] = (int) Pledge.OutputType,
                ["OwnerType"] = (int) Pledge.OwnerType,
                ["PledgeNum"] = "",
                ["UnsignedCorporationId"] = "",
                ["UnsignedCorporationId_input"] = "",
                ["UnsignedCorporationName"] = "",
            };
        }
    }
}