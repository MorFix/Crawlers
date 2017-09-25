using System.Net.Http;

namespace Crawlers.Infra
{
    public interface ICrawlingContext
    {
        HttpClientHandler HttpHandler { get; set; }
        HttpClient Client { get; set; }
        object Result { get; set; }
        void Set(string key, object value);
        T Get<T>(string key) where T : class;
        
    }
}