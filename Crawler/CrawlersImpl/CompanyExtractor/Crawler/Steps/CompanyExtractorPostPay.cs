using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.CompanyExtractor.Crawler.Steps
{
    public class CompanyExtractorPostPay : ICrawlingStep
    {
        public async Task Execute(ICrawlingContext context)
        {
            SetClientToIca(context);

            using (var content = GetQueryString(context))
            {
                var strContent = await content.ReadAsStringAsync();
                var path = context.Get<Uri>("downloadPath").ToString();

                var doc = await CrawlingHelper.GetHtmlDocument(context.Client, $"{path}?{strContent}");
                var fileId = doc.GetElementbyId("RequestId").GetAttributeValue("value", null);

                SetCrawlerResult(context, fileId);
            }
        }

        private void SetCrawlerResult(ICrawlingContext context, string fileId)
        {
            var downloadLink = new UriBuilder(context.Client.BaseAddress)
            {
                Path = "/Common/GetFile/",
                Query = "companyExtract=true&fileName=" + fileId
            };

            context.Result = downloadLink.ToString();
        }

        private FormUrlEncodedContent GetQueryString(ICrawlingContext context)
        {
            return new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("enc_string", context.Get<string>("encryptionString"))
            });
        }

        private void SetClientToIca(ICrawlingContext context)
        {
            context.Client = new HttpClient(context.HttpHandler)
            {
                BaseAddress = new Uri("https://ica.justice.gov.il/"),
            };
        }
    }
}