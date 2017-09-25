using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorSaveDetails : ICrawlingStep
    {
        private CompanyViewModel Company { get; }

        public CompanyExtractorSaveDetails(CompanyViewModel company)
        {
            Company = company;
        }

        public async Task Execute(ICrawlingContext context)
        {
            var content = new FormUrlEncodedContent(BuildFormData(context));

            await context.Client.PostAsync("/CompanyExtract/SaveCompanyExtractDetails", content);
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData(ICrawlingContext context)
        {
            var company = context.Get<JObject>("company");

            return new[]
            {
                new KeyValuePair<string, string>("IdCorporationNumber", ""),
                new KeyValuePair<string, string>("TollRate", "10.00"),
                new KeyValuePair<string, string>("KodAgra", "369"),
                new KeyValuePair<string, string>("State", "0"),
                new KeyValuePair<string, string>("Count", "0"),
                new KeyValuePair<string, string>("IsCompany", "False"),
                new KeyValuePair<string, string>("CorporationStatus", ""),
                new KeyValuePair<string, string>("CorporationName", ""),
                new KeyValuePair<string, string>("CorporationNameEnglish", ""),
                new KeyValuePair<string, string>("SearchDetailsResultsList[0].IdCorporationNumber", Company.Id),
                new KeyValuePair<string, string>("SearchDetailsResultsList[0].CorporationName", company["corporationName"].Value<string>()),
                new KeyValuePair<string, string>("SearchDetailsResultsList[0].SlaveryDetails", "כולל שעבודים"),
                new KeyValuePair<string, string>("SearchDetailsResultsList[0].TollRate", "10"),
                new KeyValuePair<string, string>("SearchDetailsResultsList[0].State", "2"),
                new KeyValuePair<string, string>("SaveUrl", "/CompanyExtract/SaveCompanyExtractDetails"),
                new KeyValuePair<string, string>("SaveAnyWay", "False"),
                new KeyValuePair<string, string>("tab_ignorevalidationgroup", "IdCorporationNumberGrp;SearchDetailsGrp"),
                new KeyValuePair<string, string>("tab_acceptvalidationgroup", ""),
                new KeyValuePair<string, string>("RequestType", "32"),
                new KeyValuePair<string, string>("ObjectState", "true"),
                new KeyValuePair<string, string>("currentJSFunction", "False"),
            };
        }
    }
}