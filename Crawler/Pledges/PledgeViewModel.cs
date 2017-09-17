using Crawlers.Infra;

namespace Crawlers.Pledges
{
    public class PledgeViewModel : IEcomDetails
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public PledgeOutputType OutputType { get; set; }
    }
}