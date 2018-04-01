using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Crawlers.Infra
{
    public class DefaultCrawlingContext : ICrawlingContext
    {
        public HttpClientHandler HttpHandler { get; set; }
        public HttpClient Client { get; set; }
        public object Result { get; set; }

        private Dictionary<string, object> Data { get; }

        public DefaultCrawlingContext()
        {
            HttpHandler = new HttpClientHandler();

            Client = new HttpClient(HttpHandler);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            Data = new Dictionary<string, object>();
            Result = "OK";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public void Set(string key, object value)
        {
            Data[key] = value;
        }

        public T Get<T>(string key) where T : class
        {
            object result;
            Data.TryGetValue(key, out result);

            return result as T;
        }
    }
}