using System;
using System.Linq;
using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.Pledges.Crawler.Steps
{
    public class PledgesEncryption : ICrawlingStep
    {
        public async Task Execute(ICrawlingContext context)
        {
            var doc = await CrawlingHelper.GetHtmlDocument(context.Client, "/General/Shoam/?guidKey=" + context.Get<string>("guidKey"));
            var encryptionString = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "input")?.GetAttributeValue("value", null);

            if (string.IsNullOrWhiteSpace(encryptionString))
            {
                throw new Exception("Error during encryption process");    
            }

            context.Set("enc_string", encryptionString);
        }
    }
}