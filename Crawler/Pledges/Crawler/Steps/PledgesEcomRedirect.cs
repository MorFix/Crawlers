using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.Pledges.Crawler.Steps
{
    public class PledgesEcomRedirect : ICrawlingStep
    {
        public async Task Execute(ICrawlingContext context)
        {
            MoveToEcomClient(context);
            var response = await GetEcomRedirectResponse(context);

            string redirectFile = null;
            try
            {
                redirectFile = response.Headers.Location?.Segments.LastOrDefault();
            }
            catch (Exception)
            {
                //ignore
            }

            EnsureRedirectFile(redirectFile);
        }

        private async Task<HttpResponseMessage> GetEcomRedirectResponse(ICrawlingContext context)
        {
            var response = await context.Client.PostAsync(
                "/Counter/general/Direction.aspx?counter=53&catalog=1&category=JusticePayments_1_BrowseOnlinePayment&language=he",
                new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("enc_string", context.Get<string>("enc_string"))
                }));
            return response;
        }

        private void MoveToEcomClient(ICrawlingContext context)
        {
            context.Client = new HttpClient(context.HttpHandler)
            {
                BaseAddress = new Uri("https://ecom.gov.il")
            };
        }

        private void EnsureRedirectFile(string redirectFile)
        {
            if (redirectFile == null || !redirectFile.Equals("DetailedBasket.aspx"))
            {
                throw new Exception("Unknown error");
            }
        }
    }
}