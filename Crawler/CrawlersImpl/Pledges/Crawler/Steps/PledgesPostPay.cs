using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using HtmlAgilityPack;

namespace Crawlers.CrawlersImpl.Pledges.Crawler.Steps
{
    public class PledgesPostPay : ICrawlingStep
    {
        public async Task Execute(ICrawlingContext context)
        {
            var selfSendResponse = await GetSelfSendResponseDocument(context);
            var docHelper = new EcomDocumentHelper(selfSendResponse);
            var path = docHelper.GetFirstFormAction();

            using (var content = GetRedirectQueryString(path, docHelper.GetEncryptionString()))
            {
                var queryString = await content.ReadAsStringAsync();

                context.Result = $"<a href='{path}?{queryString}'>Link to file</a>";
            }
        }

        private async Task<HtmlDocument> GetSelfSendResponseDocument(ICrawlingContext context)
        {
            var path = context.Get<Uri>("downloadPath");

            var doc = await CrawlingHelper.GetHtmlDocument(context.Client, path);
            CrawlingHelper.SetEventParams(context, doc);

            var selfResponse = await context.Client.PostAsync(path, CreateSelfSendContent(context));

            var selfResponseDoc = new HtmlDocument();
            selfResponseDoc.LoadHtml(await selfResponse.Content.ReadAsStringAsync());

            return selfResponseDoc;
        }

        private HttpContent CreateSelfSendContent(ICrawlingContext context)
        {
            return new CustomFormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("__VIEWSTATE", context.Get<string>("viewState")),
                new KeyValuePair<string, string>("__EVENTVALIDATION", context.Get<string>("eventValidation")),
                new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", context.Get<string>("viewStateGenerator")),
                new KeyValuePair<string, string>("ReceiptLinkImage.x", "55"),
                new KeyValuePair<string, string>("ReceiptLinkImage.y", "6"),
            });
        }

        private HttpContent GetRedirectQueryString(string url, string encryptionString)
        {
            return new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("purl", url), 
                new KeyValuePair<string, string>("enc_string", encryptionString) 
            });
        }
    }
}