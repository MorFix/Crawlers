using System.Collections.Generic;
using System.Net.Http;

namespace Crawlers.Infra
{
    public class DefaultCrawlingContext : ICrawlingContext
    {
        public HttpClientHandler HttpHandler { get; }
        public HttpClient Client { get; }

        private Dictionary<string, object> Data { get; }

        public DefaultCrawlingContext()
        {
            HttpHandler = new HttpClientHandler(); 
            Client = new HttpClient(HttpHandler);
            Data = new Dictionary<string, object>();
        }

        public void Set(string key, object value)
        {
            Data[key] = value;
        }

        public T Get<T>(string key) where T : class
        {
            Data.TryGetValue(key, out var result);

            return result as T;
        }
    }
}