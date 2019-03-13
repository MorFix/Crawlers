using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using HtmlAgilityPack;

namespace Crawlers.CrawlersImpl.Tabu.Crawler.Steps
{
    public class TabuSendData : ICrawlingStep
    {
        private const string Url = "/Counter/alternative/tabuNesach/Homepage.aspx?counter=10";

        private NesachViewModel Nesach { get; }

        public TabuSendData(NesachViewModel nesach)
        {
            Nesach = nesach;
        }

        public async Task Execute(ICrawlingContext context)
        {           
            var response = await context.Client.PostAsync(Url, new CustomFormUrlEncodedContent(BuildFormData(context)));

            await EnsureRedirectFile(response.Headers.Location, context);
        }

        private async Task EnsureRedirectFile(Uri location, ICrawlingContext context)
        {
            var redirectFile = CrawlingHelper.TryGetRedirectFile(location);

            if (redirectFile == null)
            {
                throw new Exception("Unknown error");
            }

            if (!redirectFile.Equals("DetailedBasket.aspx"))
            {
                var doc = await CrawlingHelper.GetHtmlDocument(context.Client, location);
                var errorString = GetEcomError(doc) ?? "No match found";
                
                throw new Exception(errorString);
            }
        }

        private string GetEcomError(HtmlDocument ecomResponse)
        {
            try
            {
                var td = GetErrorTd(ecomResponse);

                td.ChildNodes[1].Remove();

                return td.InnerText;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private HtmlNode GetErrorTd(HtmlDocument doc)
        {
            return doc.GetElementbyId("backbutton").ParentNode
                                                   .ChildNodes[1]
                                                   .ChildNodes[1]
                                                   .ChildNodes[1]
                                                   .ChildNodes[3]
                                                   .ChildNodes[1];
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData(ICrawlingContext context)
        {
            return new[]
            {
                new KeyValuePair<string, string>("__EVENTTARGET", "m_lbAcceptBtn"),
                new KeyValuePair<string, string>("__EVENTARGUMENT", ""),
                new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", "97D0459B"),
                new KeyValuePair<string, string>("m_ddlNesachTypeBooks", "F"),
                new KeyValuePair<string, string>("m_ddlNameBook", "1013"),
                new KeyValuePair<string, string>("auth", "G"), // Search by Gush/Helka (G) or book (F)
                new KeyValuePair<string, string>("rowInputText_rowBookNum_0_0", "111111111"),
                new KeyValuePair<string, string>("rowInputText_rowPageNum_0_0", "11111"),
                new KeyValuePair<string, string>("m_ddlNesachType", Nesach.Type.ToString()),
                new KeyValuePair<string, string>("rowInputText_rowGush_0_0", Nesach.Gush),
                new KeyValuePair<string, string>("rowInputText_rowHelka_0_0", Nesach.Helka),
                new KeyValuePair<string, string>("rowInputText_rowTatHelka_0_0", Nesach.SubHelka),
                new KeyValuePair<string, string>("__VIEWSTATE", context.Get<string>("viewState")),
                new KeyValuePair<string, string>("__EVENTVALIDATION", context.Get<string>("eventValidation")),
            };
        }
    }
}