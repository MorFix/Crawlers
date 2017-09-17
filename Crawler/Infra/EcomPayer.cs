using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crawlers.Infra
{
    internal class EcomPayer
    {
        private string Url { get; }
        private ICrawlingContext Context { get; }
        private IEcomDetails ViewModel { get; }

        public EcomPayer(string url, ICrawlingContext context, IEcomDetails viewModel)
        {
            Url = url;
            Context = context;
            ViewModel = viewModel;
        }

        public async Task Pay()
        {
            var doc = await CrawlingHelper.GetHtmlDocument(Context.Client, Url);
            CrawlingHelper.SetEventParams(Context, doc);

            var hidStepGuid = doc.GetElementbyId("hidStepGuid")?.GetAttributeValue("value", null);
            Context.Set("hidStepGuid", hidStepGuid);

            var response = await Context.Client.PostAsync(Url, new FormUrlEncodedContent(BuildFormData(Context)));
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

        private void EnsureRedirectFile(string redirectFile)
        {
            if (redirectFile == null)
            {
                throw new Exception("Unknown error - Check your configuration");
            }

            if (!redirectFile.Equals("Order.aspx"))
            {
                throw new Exception("Payment was unsuccessful");
            }
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData(ICrawlingContext context)
        {
            return new[]
            {
                new KeyValuePair<string, string>("__EVENTTARGET", ""),
                new KeyValuePair<string, string>("__EVENTARGUMENT", ""),
                new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", "A42C1B88"),
                new KeyValuePair<string, string>("__VIEWSTATE", context.Get<string>("viewState")),
                new KeyValuePair<string, string>("__EVENTVALIDATION", context.Get<string>("eventValidation")),
                new KeyValuePair<string, string>("hidStepGuid", context.Get<string>("hidStepGuid")),
                new KeyValuePair<string, string>("chkBoxEula", "on"),
                new KeyValuePair<string, string>("rbCardType", "on"),
                new KeyValuePair<string, string>("rbReceipientType", "on"),
                new KeyValuePair<string, string>("payerPhone", ""),
                new KeyValuePair<string, string>("inputAutomaticCC", ""),
                new KeyValuePair<string, string>("CvvNumberTextBox", ""),
                new KeyValuePair<string, string>("CardOwnerNameTextbox", ""),
                new KeyValuePair<string, string>("creditTextBox", ""),
                new KeyValuePair<string, string>("sapakTextBox", ""),
                new KeyValuePair<string, string>("paymentTypeTextBox", ""),
                new KeyValuePair<string, string>("payImg2.x", "681"),
                new KeyValuePair<string, string>("payImg2.y", "430"),

                new KeyValuePair<string, string>("receiptName", ViewModel.Name ?? ConfigurationManager.AppSettings["name"]),
                new KeyValuePair<string, string>("receiptEmail", ViewModel.Email ?? ConfigurationManager.AppSettings["email"]),
                new KeyValuePair<string, string>("cardNumberTextBox", ConfigurationManager.AppSettings["cardNumber"]),
                new KeyValuePair<string, string>("yearDropDown", ConfigurationManager.AppSettings["expirationYear"]),
                new KeyValuePair<string, string>("monthDropDown", ConfigurationManager.AppSettings["expirationMonth"]),
                new KeyValuePair<string, string>("IdNumberTextBox", ConfigurationManager.AppSettings["idNumber"]),


            };
        }
    }
}