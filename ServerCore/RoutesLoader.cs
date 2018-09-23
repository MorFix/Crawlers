using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace ServerCore
{
    public class RoutesLoader : DefaultAssembliesResolver
    {
        public static ICollection<Assembly> AllAssemblies = new DefaultAssembliesResolver()
                                                                .GetAssemblies()
                                                                .Concat(GetCustomAssemblies())
                                                                .ToList();

        public override ICollection<Assembly> GetAssemblies()
        {
            return AllAssemblies;
        }

        private static ICollection<Assembly> GetCustomAssemblies()
        {
            return (ConfigurationManager.AppSettings["routingAssemblies"] ?? string.Empty).Split(',')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(Assembly.LoadFrom)
                .ToList();
        }
    }
}