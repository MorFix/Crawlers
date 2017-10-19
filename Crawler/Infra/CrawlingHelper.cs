using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawlers.Infra
{
    public class CrawlingHelper
    {
        public static Task<HtmlDocument> GetHtmlDocument(HttpClient client, Uri url)
        {
            return GetHtmlDocument(client, url.ToString());
        }

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
            context.Set("viewStateGenerator", document.GetElementbyId("__VIEWSTATEGENERATOR")?.GetAttributeValue("value", null));
        }

        public static string TryGetRedirectFile(Uri locationHeader)
        {
            try
            {
                return locationHeader?.Segments.LastOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IDictionary<string, string> GetQueryString(HttpRequestMessage request)
        {
            return request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);
        }

        public static string ExtractEncryptionString(HtmlDocument doc)
        {
            return doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "enc_string")?.GetAttributeValue("value", null);
        }
    }
}