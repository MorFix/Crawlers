using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;
using ServerCore;

namespace WebServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Formatters. JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Services.Replace(typeof(IAssembliesResolver), new RoutesLoader());
            config.MapHttpAttributeRoutes(new InheritanceDirectRouteProvider());
            config.Filters.Add(new ExceptionsFilter());

            config.EnsureInitialized();

            app.UseWebApi(config);
        }
    }
}