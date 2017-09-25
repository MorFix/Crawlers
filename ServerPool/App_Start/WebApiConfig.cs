using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using ServerPool.Filters;
using ServerPool.MessageHandlers;
using ServerPool.Services;

namespace ServerPool
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Services.Replace(typeof(IAssembliesResolver), new RoutesLoader());
            config.Filters.Add(new ExceptionsFilter());
            config.MessageHandlers.Add(new PasswordHandler(ConfigurationManager.AppSettings["password"]));

            // Web API routes
            config.MapHttpAttributeRoutes(new InheritanceDirectRouteProvider());
        }
    }
}
