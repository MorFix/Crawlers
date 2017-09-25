using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.CrawlersImpl.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorAddToBasket : ICrawlingStep
    {
        private CompanyViewModel Company { get; }

        public CompanyExtractorAddToBasket(CompanyViewModel company)
        {
            Company = company;
        }

        public async Task Execute(ICrawlingContext context)
        {
            var content = new FormUrlEncodedContent(BuildFormData(context));
            var response = await context.Client.PostAsync("/CompanyExtract/AddSearchDetailsToList", content);

            var addToBasketResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            var error = addToBasketResponse["Errors"].Value<JArray>().FirstOrDefault();
            if (error != null)
            {
                throw new Exception(error["ErrorMessage"].Value<string>());
            }
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData(ICrawlingContext context)
        {
            var company = context.Get<JObject>("company");

            return new[]
            {
                new KeyValuePair<string, string>("IdCorporationNumber", Company.Id),
                new KeyValuePair<string, string>("TollRate", "10.00"),
                new KeyValuePair<string, string>("KodAgra", "369"),
                new KeyValuePair<string, string>("State", "0"),
                new KeyValuePair<string, string>("Count", "0"),
                new KeyValuePair<string, string>("IsCompany", "False"),
                new KeyValuePair<string, string>("CorporationStatus", company["status"].Value<string>()),
                new KeyValuePair<string, string>("CorporationName", company["corporationName"].Value<string>()),
                new KeyValuePair<string, string>("CorporationNameEnglish", company["englishCorporationName"].Value<string>()),
                new KeyValuePair<string, string>("Slavery", "1"),
                new KeyValuePair<string, string>("SaveUrl", "/CompanyExtract/SaveCompanyExtractDetails"),
                new KeyValuePair<string, string>("SaveAnyWay", "False"),
                new KeyValuePair<string, string>("tab_ignorevalidationgroup", "IdCorporationNumberGrp;SearchDetailsGrp"),
                new KeyValuePair<string, string>("tab_acceptvalidationgroup", ""),
                new KeyValuePair<string, string>("RequestType", "32"),
                new KeyValuePair<string, string>("ObjectState", "true"),
                new KeyValuePair<string, string>("currentJSFunction", "False"),
                new KeyValuePair<string, string>("X-Requested-With", "XMLHttpRequest"),
            };
        }
    }
}