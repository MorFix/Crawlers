using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Crawlers.Pledges;

namespace Crawlers.CompanyExtractor
{
    [RoutePrefix("api/companyExtractor")]
    public class CompanyExtractorController : ApiController
    {
        private readonly CompanyExtractorLogic _logic;

        public CompanyExtractorController()
        {
            _logic = new CompanyExtractorLogic();
        }


        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> Purchase()
        {
            var queryString = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);
            await _logic.PurchaseAsync(queryString);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("OK")
            };
        }
    }
}