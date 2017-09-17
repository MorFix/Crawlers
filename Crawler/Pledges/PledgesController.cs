using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Crawlers.Pledges
{
    [RoutePrefix("api/pledges")]
    public class PledgesController : ApiController
    {
        private readonly PledgesLogic _logic;

        public PledgesController()
        {
            _logic = new PledgesLogic();
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