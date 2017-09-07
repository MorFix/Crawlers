using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ServerPool.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new ErrorContent(actionExecutedContext.Exception)
            };
        }
    }
}