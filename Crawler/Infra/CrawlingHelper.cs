using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawlers.Infra
{
    public class CrawlingHelper
    {
        public static async Task<HtmlDocument> GetHtmlDocument(HttpClient client, string url)
        {
            var rawResponse = await client.GetAsync(url);
            var htmlContent = await rawResponse.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            return doc;
        }

        public static void SetEventParams(ICrawlingContext context, HtmlDocument document)
        {
            context.Set("viewState", document.GetElementbyId("__VIEWSTATE")?.GetAttributeValue("value", null));
            context.Set("eventValidation", document.GetElementbyId("__EVENTVALIDATION")?.GetAttributeValue("value", null));
        }

        public static Task PayOnEcom(string url, ICrawlingContext context, IEcomDetails details)
        {
            return new EcomPayer(url, context, details).Pay();
        }
    }
}