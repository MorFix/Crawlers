using System;
using System.Collections.Generic;

namespace Crawlers.Infra
{
    public class ServicesFactory
    {
        private static Dictionary<Type, Func<object>> Services { get; }

        static ServicesFactory()
        {
            Services = new Dictionary<Type, Func<object>>();
            RegisterDefaultServices();
        }

        private static void RegisterDefaultServices()
        {
            Register(typeof(ICrawlingContext), () => new DefaultCrawlingContext());
        }

        public static void Register(Type type, Func<object> factory)
        {
            Services[type] = factory;
        }

        public static TService GetService<TService>() where TService : class
        {
            return Services[typeof(TService)]() as TService;
        }
    }
}