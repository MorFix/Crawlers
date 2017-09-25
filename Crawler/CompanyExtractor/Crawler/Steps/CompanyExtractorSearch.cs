using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorSearch : ICrawlingStep
    {
        private CompanyViewModel Company { get; }

        public CompanyExtractorSearch(CompanyViewModel company)
        {
            Company = company;
        }

        public async Task Execute(ICrawlingContext context)
        {
            context.Client.BaseAddress = new Uri("https://ica.justice.gov.il/");
            context.HttpHandler.AllowAutoRedirect = false;

            // Registering the session as valid
            await context.Client.GetAsync("/Request/OpenRequest?rt=CompanyExtract");

            var companyExistsResponse = await context.Client.PostAsync("/CompanyExtract/GetIsraeliCorporation",
                                                                       new FormUrlEncodedContent(BuildFormData()));

            var jsonResponse = JObject.Parse(await companyExistsResponse.Content.ReadAsStringAsync());
            if (!jsonResponse["valid"].Value<bool>())
            {
                throw new Exception("Company not found");
            }

            context.Set("company", jsonResponse);
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData()
        {
            return new[]
            {
                new KeyValuePair<string, string>("IdCorporationNumber", Company.Id),
                new KeyValuePair<string, string>("TollRate", "10.00"),
                new KeyValuePair<string, string>("KodAgra", "369"),
                new KeyValuePair<string, string>("State", "0"),
                new KeyValuePair<string, string>("Count", "0"),
                new KeyValuePair<string, string>("IsCompany", "False"),
                new KeyValuePair<string, string>("CorporationStatus", ""),
                new KeyValuePair<string, string>("CorporationName", ""),
                new KeyValuePair<string, string>("CorporationNameEnglish", ""),
                new KeyValuePair<string, string>("SaveUrl", "/CompanyExtract/SaveCompanyExtractDetails"),
                new KeyValuePair<string, string>("SaveAnyWay", "False"),
                new KeyValuePair<string, string>("tab_ignorevalidationgroup", "IdCorporationNumberGrp;SearchDetailsGrp"),
                new KeyValuePair<string, string>("tab_acceptvalidationgroup", ""),
                new KeyValuePair<string, string>("RequestType", "32"),
                new KeyValuePair<string, string>("ObjectState", "true"),
                new KeyValuePair<string, string>("currentJSFunction", "False"),
                new KeyValuePair<string, string>("X-Requested-With", "XMLHttpRequest")
            };
        }
    }
}