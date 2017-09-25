using System;
using System.Threading.Tasks;

namespace Crawlers.Infra.Ecom
{
    public abstract class EcomHandler : ICrawlingStep
    {
        protected abstract Uri EcomTarget { get; }
        protected abstract Uri PaymentUrl { get; }

        protected abstract Uri GetConnectionEndpoint(ICrawlingContext context);

        private BaseViewModel ViewModel { get; }

        protected EcomHandler(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        
        public async Task Execute(ICrawlingContext context)
        {
            await new EcomRedirector(context, EcomTarget, GetConnectionEndpoint).Redirect();
            await new EcomPayer(context, PaymentUrl, ViewModel).Pay();
        }
    }
}