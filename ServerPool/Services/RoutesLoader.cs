using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace ServerPool.Services
{
    public class RoutesLoader : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            return (ConfigurationManager.AppSettings["routingAssemblies"] ?? string.Empty).Split(',')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(Assembly.LoadFrom)
                .Concat(base.GetAssemblies())
                .ToList();
        }
    }
}