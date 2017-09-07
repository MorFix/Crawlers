using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServerPool.MessageHandlers
{
    public class PasswordHandler : DelegatingHandler
    {
        private string Password { get; }

        public PasswordHandler(string password)
        {
            Password = password;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var queryString = request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);

            // ReSharper disable once InlineOutVariableDeclaration
            string requestPassword;
            queryString.TryGetValue("password", out requestPassword);

            if (!string.IsNullOrWhiteSpace(Password) && requestPassword != Password)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new ErrorContent("Invalid password")
                });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}