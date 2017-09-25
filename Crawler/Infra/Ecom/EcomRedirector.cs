using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crawlers.Infra.Ecom
{
    public class EcomRedirector
    {
        private ICrawlingContext Context { get; }
        private Uri EcomTarget { get; }
        private Func<ICrawlingContext, Uri> GetConnectionEndpoint { get; }

        public EcomRedirector(ICrawlingContext context, Uri ecomTarget, Func<ICrawlingContext, Uri> getConnectionEndpoint)
        {
            Context = context;
            EcomTarget = ecomTarget;
            GetConnectionEndpoint = getConnectionEndpoint;
        }

        public async Task Redirect()
        {
            var encryptionString = await GetEncryptionString(Context);
            if (string.IsNullOrWhiteSpace(encryptionString))
            {
                throw new Exception("Error during payment encryption process");
            }

            MoveToEcomClient(Context);

            var response = await GetEcomRedirectResponse(Context, encryptionString);
            EnsureRedirectFile(CrawlingHelper.TryGetRedirectFile(response.Headers.Location));
        }

        private async Task<string> GetEncryptionString(ICrawlingContext context)
        {
            var doc = await CrawlingHelper.GetHtmlDocument(context.Client, GetConnectionEndpoint(context));

            return doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "input")?.GetAttributeValue("value", null);
        }

        private async Task<HttpResponseMessage> GetEcomRedirectResponse(ICrawlingContext context, string encryptionString)
        {
            var response = await context.Client.PostAsync(EcomTarget,
                new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("enc_string", encryptionString)
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