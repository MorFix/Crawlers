using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.CrawlersImpl.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorSendData : ICrawlingStep
    {
        private CompanyViewModel Company { get; }

        public CompanyExtractorSendData(CompanyViewModel company)
        {
            Company = company;
        }

        public async Task Execute(ICrawlingContext context)
        {
            var response = await context.Client.PostAsync("/CompanyExtract/ApproveCompanyExtract", new FormUrlEncodedContent(BuildFormData()));
            var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            context.Set("requestId", jsonResponse["RequestId"].Value<string>());
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData()
        {
            return new[]
            {
                new KeyValuePair<string, string>("ConnectDetailsViewModel.Email", Company.Email),
                new KeyValuePair<string, string>("ConnectDetailsViewModel.ConfirmWebsiteConditions", "true"),
                new KeyValuePair<string, string>("SaveUrl", "/CompanyExtract/SaveConnectDetails"),
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