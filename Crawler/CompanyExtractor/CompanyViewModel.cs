using Crawlers.Infra;

namespace Crawlers.CompanyExtractor
{
    public class CompanyViewModel : IEcomDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}