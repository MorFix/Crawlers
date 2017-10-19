using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawlers.Infra.Ecom
{
    public class EcomPayer
    {
        private ICrawlingContext Context { get; }
        private Uri PaymentUrl { get; }
        private BaseViewModel ViewModel { get; }

        public EcomPayer(ICrawlingContext context, Uri paymentUrl, BaseViewModel viewModel)
        {
            PaymentUrl = paymentUrl;
            Context = context;
            ViewModel = viewModel;
        }

        public async Task Pay()
        {
            var doc = await CrawlingHelper.GetHtmlDocument(Context.Client, PaymentUrl);
            CrawlingHelper.SetEventParams(Context, doc);

            var hidStepGuid = doc.GetElementbyId("hidStepGuid")?.GetAttributeValue("value", null);
            var response = await Context.Client.PostAsync(PaymentUrl, new FormUrlEncodedContent(BuildFormData(hidStepGuid)));

            await EnsureRedirectFile(response);
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData(string hidStepGuid)
        {
            return new[]
            {
                new KeyValuePair<string, string>("__EVENTTARGET", ""),
                new KeyValuePair<string, string>("__EVENTARGUMENT", ""),
                new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", "A42C1B88"),
                new KeyValuePair<string, string>("__VIEWSTATE", Context.Get<string>("viewState")),
                new KeyValuePair<string, string>("__EVENTVALIDATION", Context.Get<string>("eventValidation")),
                new KeyValuePair<string, string>("hidStepGuid", hidStepGuid),
                new KeyValuePair<string, string>("chkBoxEula", "on"),
                new KeyValuePair<string, string>("rbCardType", "on"),
                new KeyValuePair<string, string>("rbReceipientType", "on"),
                new KeyValuePair<string, string>("payerPhone", ""),
                new KeyValuePair<string, string>("inputAutomaticCC", ""),
                new KeyValuePair<string, string>("CardOwnerNameTextbox", ""),
                new KeyValuePair<string, string>("creditTextBox", ""),
                new KeyValuePair<string, string>("sapakTextBox", ""),
                new KeyValuePair<string, string>("paymentTypeTextBox", ""),
                new KeyValuePair<string, string>("payImg2.x", "681"),
                new KeyValuePair<string, string>("payImg2.y", "430"),

                new KeyValuePair<string, string>("receiptName", ViewModel.Name),
                new KeyValuePair<string, string>("receiptEmail", ViewModel.Email),
                new KeyValuePair<string, string>("cardNumberTextBox", ConfigurationManager.AppSettings["cardNumber"]),
                new KeyValuePair<string, string>("yearDropDown", ConfigurationManager.AppSettings["expirationYear"]),
                new KeyValuePair<string, string>("monthDropDown", ConfigurationManager.AppSettings["expirationMonth"]),
                new KeyValuePair<string, string>("IdNumberTextBox", ConfigurationManager.AppSettings["idNumber"]),
                new KeyValuePair<string, string>("CvvNumberTextBox", ConfigurationManager.AppSettings["cvv"]),
            };
        }

        private async Task EnsureRedirectFile(HttpResponseMessage response)
        {
            var redirectFile = CrawlingHelper.TryGetRedirectFile(response.Headers.Location);
            if (redirectFile == null)
            {
                TryJsRedirect(await response.Content.ReadAsStreamAsync());

                return;
            }

            if (!redirectFile.Equals("Order.aspx"))
            {
                throw new Exception("Payment was unsuccessful");
            }

            Context.Set("downloadPath", response.Headers.Location);
        }

        private void TryJsRedirect(Stream content)
        {
            var doc = new HtmlDocument();
            doc.Load(content);
            var docHelper = new EcomDocumentHelper(doc);

            var actionUrl = docHelper.GetFirstFormAction();
            if (actionUrl == null || actionUrl.StartsWith("Payment.aspx"))
            {
                throw new Exception("Unknown error - Check your configuration");
            }

            Context.Set("downloadPath", new Uri(new Uri(actionUrl).LocalPath, UriKind.Relative));
            Context.Set("encryptionString", docHelper.GetEncryptionString());
        }
    }
}