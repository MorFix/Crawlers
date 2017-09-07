using System.Net.Http;

namespace Crawlers.Infra
{
    public interface ICrawlingContext
    {
        HttpClientHandler HttpHandler { get; }
        HttpClient Client { get; }
        void Set(string key, object value);
        T Get<T>(string key) where T : class;
    }
}