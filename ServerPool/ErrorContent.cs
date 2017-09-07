using System;
using System.Net.Http;
using System.Text;

namespace ServerPool
{
    public class ErrorContent : StringContent
    {
        public ErrorContent(Exception ex) : base(GetMessage(ex))
        {
        }

        public ErrorContent(string content) : base(GetMessage(content))
        {
        }

        public ErrorContent(string content, Encoding encoding) : base(GetMessage(content), encoding)
        {
        }

        public ErrorContent(string content, Encoding encoding, string mediaType) : base(GetMessage(content), encoding, mediaType)
        {
        }

        private static string GetMessage(Exception ex)
        {
            return GetMessage(ex.Message);
        }

        private static string GetMessage(string error)
        {
            return $"Error: {error}";
        }
    }
}