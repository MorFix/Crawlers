using Crawlers.CrawlersImpl.Pledges.Enums;
using Crawlers.Infra.Ecom;

namespace Crawlers.CrawlersImpl.Pledges
{
    public class PledgeViewModel : BaseViewModel
    {
        // Widely-common properties
        public PledgeViewType ViewType { get; set; }

        // Common properties
        public PledgeOutputType OutputType { get; set; }

        // "By Owner" Properties
        public PledgeOwnerType OwnerType { get; set; }
        public string Id { get; set; }
        public int BankNumber { get; set; }

        // "By Property" Properties
        public PledgesAssetType AssetType { get; set; }
        public string LicenseNumber { get; set; }
    }
}