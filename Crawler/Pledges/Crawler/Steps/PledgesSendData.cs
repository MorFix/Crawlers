using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Crawlers.Infra;
using Newtonsoft.Json.Linq;

namespace Crawlers.Pledges.Crawler.Steps
{
    public class PledgesSendData : ICrawlingStep
    {
        private PledgeViewModel Pledge { get; }

        public PledgesSendData(PledgeViewModel pledge)
        {
            Pledge = pledge;
        }

        public async Task Execute(ICrawlingContext context)
        {
            const string errorsKey = "Errors";

            var formData = BuildFormData(context);

            var response =
                await context.Client.PostAsync("/Search/PledgeBrowsePayment", new FormUrlEncodedContent(formData));

            var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (responseObject[errorsKey].HasValues)
            {
                throw new Exception(responseObject[errorsKey].Value<JArray>()["ErrorMessage"].Value<string>());
            }

            context.Set("guidKey", responseObject["inputStr"].Value<string>());
        }

        private IEnumerable<KeyValuePair<string, string>> BuildFormData(ICrawlingContext context)
        {
            return new[]
            {
                new KeyValuePair<string, string>("IsGetResultsDelayed", "0"),
                new KeyValuePair<string, string>("ByType", "1"),
                new KeyValuePair<string, string>("OwnerType", ""),
                new KeyValuePair<string, string>("IdNum", ""),
                new KeyValuePair<string, string>("IdNumRequired", ""),
                new KeyValuePair<string, string>("IdForeignResident", ""),
                new KeyValuePair<string, string>("CountryId_input", ""),
                new KeyValuePair<string, string>("CountryId", ""),
                new KeyValuePair<string, string>("CountryName", ""),
                new KeyValuePair<string, string>("BankId_input", ""),
                new KeyValuePair<string, string>("BankId", ""),
                new KeyValuePair<string, string>("BankName_input", ""),
                new KeyValuePair<string, string>("BankName", ""),
                new KeyValuePair<string, string>("BankNameString", ""),
                new KeyValuePair<string, string>("UnsignedCorporationId_input", ""),
                new KeyValuePair<string, string>("UnsignedCorporationId", ""),
                new KeyValuePair<string, string>("UnsignedCorporationName", ""),
                new KeyValuePair<string, string>("CorporationName", ""),
                new KeyValuePair<string, string>("LastNameForeign", ""),
                new KeyValuePair<string, string>("FirstNameForeign", ""),
                new KeyValuePair<string, string>("ForeignCorporationName", ""),
                new KeyValuePair<string, string>("CorporationNameValue", ""),
                new KeyValuePair<string, string>("LastNameEnglish", ""),
                new KeyValuePair<string, string>("FirstNameEnglish", ""),
                new KeyValuePair<string, string>("Asset", ""),
                new KeyValuePair<string, string>("LicenseNum", ""),
                new KeyValuePair<string, string>("PledgeNum", ""),
                new KeyValuePair<string, string>("OutputType", ((int) Pledge.OutputType).ToString()),
                new KeyValuePair<string, string>("CheckBefore1995", "false"),
                new KeyValuePair<string, string>("CheckBefore1995Id_input", ""),
                new KeyValuePair<string, string>("CheckBefore1995Id", ""),
                new KeyValuePair<string, string>("LastName", ""),
                new KeyValuePair<string, string>("FirstName", ""),
                new KeyValuePair<string, string>("BrowseTollRate", "10.00"),


                new KeyValuePair<string, string>("SearchDetailsList[0].ID", ""),
                // new KeyValuePair<string, string>("SearchDetailsList[0].OutputTypeString", "משכונות פעילים"),
                new KeyValuePair<string, string>("SearchDetailsList[0].RequestBy", "חייב"),
                new KeyValuePair<string, string>("SearchDetailsList[0].RequestIdentification", "ח.פ. " + Pledge.CompanyId),
                new KeyValuePair<string, string>("SearchDetailsList[0].ResultDisplayModeString", "מיידי"),
                new KeyValuePair<string, string>("SearchDetailsList[0].BrowseTollRate", "10"),
                new KeyValuePair<string, string>("SearchDetailsList[0].ByType", "1"),
                new KeyValuePair<string, string>("SearchDetailsList[0].OwnerType", "3"),
                new KeyValuePair<string, string>("SearchDetailsList[0].IdNum", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].IdNumRequired", Pledge.CompanyId),
                new KeyValuePair<string, string>("SearchDetailsList[0].IdForeignResident", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].LastName", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].FirstName", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].LastNameForeign", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].FirstNameForeign", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].LastNameEnglish", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].FirstNameEnglish", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].CountryId", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].CountryName", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].CorporationName", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].CorporationNameValue", context.Get<string>("companyName")),
                new KeyValuePair<string, string>("SearchDetailsList[0].UnsignedCorporationId", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].UnsignedCorporationName", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].BankId", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].BankNameString", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].ForeignCorporationName", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].OutputType", ((int) Pledge.OutputType).ToString()),
                new KeyValuePair<string, string>("SearchDetailsList[0].CheckBefore1995", "false"),
                new KeyValuePair<string, string>("SearchDetailsList[0].CheckBefore1995Id", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].ResultDisplayMode", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].Asset", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].LicenseNum", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].PledgeNum", ""),
                new KeyValuePair<string, string>("SearchDetailsList[0].State", "0"),
                new KeyValuePair<string, string>("KodAgra", "168"),
                new KeyValuePair<string, string>("TollSum", "10.00"),
                new KeyValuePair<string, string>("Sum", "0"),
                new KeyValuePair<string, string>("TollRate", "10.00"),
                new KeyValuePair<string, string>("IsExempt", "False"),
                new KeyValuePair<string, string>("ContactIdPermitted", "0"),
                new KeyValuePair<string, string>("ConnectionDetails.RequestSubmitName", Pledge.Name ?? ConfigurationManager.AppSettings["name"]),
                new KeyValuePair<string, string>("ConnectionDetails.TelephoneNumber", "5555555"),
                new KeyValuePair<string, string>("ConnectionDetails.DiallingAreaNumber_input", "055"),
                new KeyValuePair<string, string>("ConnectionDetails.DiallingAreaNumber", "55"),
                new KeyValuePair<string, string>("ConnectionDetails.Email", Pledge.Email ?? ConfigurationManager.AppSettings["email"]),
                new KeyValuePair<string, string>("ConnectionDetails.AddressType", "ByEmail"),
                new KeyValuePair<string, string>("ConnectionDetails.CityId_input", ""),
                new KeyValuePair<string, string>("ConnectionDetails.CityId", ""),
                new KeyValuePair<string, string>("ConnectionDetails.StreetId_input", ""),
                new KeyValuePair<string, string>("ConnectionDetails.StreetId", ""),
                new KeyValuePair<string, string>("ConnectionDetails.House", ""),
                new KeyValuePair<string, string>("ConnectionDetails.Entrance", ""),
                new KeyValuePair<string, string>("ConnectionDetails.POBCity_input", ""),
                new KeyValuePair<string, string>("ConnectionDetails.POBCity", ""),
                new KeyValuePair<string, string>("ConnectionDetails.POB", ""),
                new KeyValuePair<string, string>("ConnectionDetails.POBZipCode", ""),
                new KeyValuePair<string, string>("ConnectionDetails.Fax", ""),
                new KeyValuePair<string, string>("ConnectionDetails.DiallingArea2Number_input", ""),
                new KeyValuePair<string, string>("ConnectionDetails.DiallingArea2Number", ""),
                new KeyValuePair<string, string>("ConnectionDetails.Apartment", ""),
                new KeyValuePair<string, string>("ConnectionDetails.Floor", ""),
                new KeyValuePair<string, string>("ConnectionDetails.ZipCode", ""),
                new KeyValuePair<string, string>("ConnectionDetails.With", ""),
                new KeyValuePair<string, string>("ConnectionDetails.DistrictSelect_input", ""),
                new KeyValuePair<string, string>("ConnectionDetails.DistrictSelect", ""),
                new KeyValuePair<string, string>("ConnectionDetails.ReadTermsOfUse", "true"),
                new KeyValuePair<string, string>("ConnectionDetails.ReadTermsOfUse", "false"),
                new KeyValuePair<string, string>("ObjectState", "true"),
            };
        }
    }
}