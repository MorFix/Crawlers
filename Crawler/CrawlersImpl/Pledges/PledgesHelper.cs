using System.Collections.Generic;
using Crawlers.CrawlersImpl.Pledges.Enums;

namespace Crawlers.CrawlersImpl.Pledges
{
    public static class PledgesHelper
    {
        public static bool IsSearchableOwnerType(PledgeOwnerType ownerType)
        {
            return new List<PledgeOwnerType>
            {
                PledgeOwnerType.Company,
                PledgeOwnerType.Association,
                PledgeOwnerType.Partnership,
                PledgeOwnerType.Sanctuary,
                PledgeOwnerType.Faction,
                PledgeOwnerType.CooperativeCompany
            }.Contains(ownerType);
        }
    }
}