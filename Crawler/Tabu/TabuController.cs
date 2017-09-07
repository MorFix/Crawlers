using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Crawlers.Tabu
{
    [RoutePrefix("api/tabu")]
    public class TabuController : ApiController
    {
        private readonly TabuLogic _logic;

        public TabuController()
        {
            _logic = new TabuLogic();
        }

        [HttpGet]
        [Route("test")]
        public HttpResponseMessage Test()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("This is a test")
            };
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