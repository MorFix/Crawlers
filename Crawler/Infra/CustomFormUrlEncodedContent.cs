using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Crawlers.Infra
{
    /// <inheritdoc />
    /// <summary>
    /// Provides a way to bypass a bug which causes FormUrlEncodedContent to fail on long content
    /// </summary>
    public class CustomFormUrlEncodedContent : StringContent
    {
        public CustomFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
            : base(CreateStringContent(nameValueCollection), null, "application/x-www-form-urlencoded")
        {
        }

        public CustomFormUrlEncodedContent(string content) : base(content)
        {
        }

        public CustomFormUrlEncodedContent(string content, Encoding encoding) : base(content, encoding)
        {
        }

        public CustomFormUrlEncodedContent(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType)
        {
        }

        private static string CreateStringContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            var stringPairs = nameValueCollection.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));

            return string.Join("&", stringPairs);
        }
    }
}