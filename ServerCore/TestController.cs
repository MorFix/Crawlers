using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerCore
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route]
        public HttpResponseMessage Test()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Join("\n", RoutesLoader.AllAssemblies.Select(x => x.GetName().Name)))
            };
        }
    }
}