using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace WebServer
{
    public class RoutesLoader : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            return ConfigurationManager.AppSettings["routingAssemblies"].Split(',')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(Assembly.LoadFrom)
                .Concat(base.GetAssemblies())
                .ToList();
        }
    }
}