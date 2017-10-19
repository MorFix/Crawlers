using System.Linq;
using HtmlAgilityPack;

namespace Crawlers.Infra
{
    public class EcomDocumentHelper
    {
        private HtmlDocument Document { get; }

        public EcomDocumentHelper(HtmlDocument doc)
        {
            Document = doc;
        }

        public string GetFirstFormAction()
        {
            return GetFirstForm()?.GetAttributeValue("action", null);
        }

        public string GetEncryptionString()
        {
            return Document.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "input")?.GetAttributeValue("value", null);
        }

        private HtmlNode GetFirstForm()
        {
            return Document.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "form");
        }
    }
}