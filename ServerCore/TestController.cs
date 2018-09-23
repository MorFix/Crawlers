using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
            var assemblies = string.Join("\n", RoutesLoader.AllAssemblies.Select(x => x.GetName().Name));
            var controllers = RoutesLoader.AllAssemblies.SelectMany(GetControllers);

            var finalContent = $"{assemblies}\n\n" +
                               $"{string.Join("\n", controllers)}\n\n" +
                               $"{Assembly.GetExecutingAssembly().GetName().Version}";

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(finalContent)
            };
        }

        private IEnumerable<string> GetControllers(Assembly asm)
        {
            return asm.GetTypes().Where(x => typeof(ApiController).IsAssignableFrom(x) && !x.IsAbstract).Select(x => x.Name);
        }
    }
}