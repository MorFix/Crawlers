using System;
using System.Collections.Generic;
using Crawlers.CrawlersImpl.Pledges.Crawler;
using Crawlers.CrawlersImpl.Pledges.Enums;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.Pledges
{
    public class PledgesLogic : BaseCrawlerLogic<PledgesCrawler, PledgeViewModel>
    {
        protected override PledgesCrawler CreateCrawler(IDictionary<string, string> parameters)
        {
            return new PledgesCrawler(CreateViewModel(parameters));
        }

        protected override PledgeViewModel CreateViewModel(IDictionary<string, string> parameters)
        {
            var pledge = base.CreateViewModel(parameters);

            parameters.TryGetValue("by", out var viewType);
            pledge.ViewType = GetViewType(viewType);

            AssignParametersByViewType(parameters, pledge);

            return pledge;
        }

        private PledgeViewType GetViewType(string viewTypeInput)
        {
            switch (viewTypeInput)
            {
                case "owner":
                    return PledgeViewType.ByOwner;
                case "property":
                    return PledgeViewType.ByProperty;
                default:
                    return 0;
            }
        }

        private void AssignParametersByViewType(IDictionary<string, string> parameters, PledgeViewModel pledge)
        {
            switch (pledge.ViewType)
            {
                case PledgeViewType.ByOwner:
                    AssignByOwnerParameters(parameters, pledge);
                    break;

                case PledgeViewType.ByProperty:
                    AssignByPropertyParameters(parameters, pledge);
                    break;

                default:
                    throw new Exception("Please set a valid 'By' parameter");
            }
        }

        private void AssignByOwnerParameters(IDictionary<string, string> parameters, PledgeViewModel pledge)
        {
            AssignOutputType(parameters, pledge);

            parameters.TryGetValue("ownerType", out var ownerType);
            pledge.OwnerType = (PledgeOwnerType) int.Parse(ownerType ?? "0");

            AssignParametersByOwnerType(parameters, pledge);
        }

        private void AssignParametersByOwnerType(IDictionary<string, string> parameters, PledgeViewModel pledge)
        {
            switch (pledge.OwnerType)
            {
                case PledgeOwnerType.IsraeliCivilian:
                case PledgeOwnerType.Company:
                case PledgeOwnerType.Association:
                case PledgeOwnerType.Partnership:
                case PledgeOwnerType.Sanctuary:
                case PledgeOwnerType.Faction:
                case PledgeOwnerType.CooperativeCompany:
                    parameters.TryGetValue("id", out var associationId);
                    pledge.Id = !string.IsNullOrWhiteSpace(associationId) ? associationId : "1";
                    break;

                case PledgeOwnerType.Bank:
                    parameters.TryGetValue("bank", out var bankNumber);
                    pledge.BankNumber = int.Parse(bankNumber ?? "0");
                    break;

                default:
                    throw new Exception("Please specify a valid Owner Type");
            }
        }

        private void AssignByPropertyParameters(IDictionary<string, string> parameters, PledgeViewModel pledge)
        {
            AssignOutputType(parameters, pledge);

            parameters.TryGetValue("assetType", out var asset);
            pledge.AssetType = (PledgesAssetType) int.Parse(asset ?? "0");

            parameters.TryGetValue("number", out var licenseNumber);
            pledge.LicenseNumber = !string.IsNullOrWhiteSpace(licenseNumber) ? licenseNumber : "1";
        }

        private void AssignOutputType(IDictionary<string, string> parameters, PledgeViewModel pledge)
        {
            parameters.TryGetValue("outputType", out var output);
            pledge.OutputType = (PledgeOutputType) int.Parse(output ?? "1");
        }
    }
}