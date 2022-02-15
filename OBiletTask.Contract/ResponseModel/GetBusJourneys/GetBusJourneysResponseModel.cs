using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.ResponseModel.GetBusJourneys
{
    public class GetBusJourneysResponseModel
    {
        public class Stop
        {
            public string name { get; set; }
            public string station { get; set; }
            public DateTime? time { get; set; }

            [JsonProperty("is-origin")]
            public bool IsOrigin { get; set; }

            [JsonProperty("is-destination")]
            public bool IsDestination { get; set; }
        }

        public class Policy
        {
            [JsonProperty("max-seats")]
            public object MaxSeats { get; set; }

            [JsonProperty("max-single")]
            public object MaxSingle { get; set; }

            [JsonProperty("max-single-males")]
            public int? MaxSingleMales { get; set; }

            [JsonProperty("max-single-females")]
            public int? MaxSingleFemales { get; set; }

            [JsonProperty("mixed-genders")]
            public bool MixedGenders { get; set; }

            [JsonProperty("gov-id")]
            public bool GovId { get; set; }
            public bool lht { get; set; }
        }

        public class Journey
        {
            public string kind { get; set; }
            public string code { get; set; }
            public List<Stop> stops { get; set; }  
            public string origin { get; set; }
            public string destination { get; set; }
            public DateTime? departure { get; set; }
            public DateTime? arrival { get; set; }
            public string currency { get; set; }
            public string duration { get; set; }

            [JsonProperty("original-price")]
            public double? OriginalPrice { get; set; }

            [JsonProperty("internet-price")]
            public double? InternetPrice { get; set; }

            [JsonProperty("provider-internet-price")]
            public object ProviderInternetPrice { get; set; }
            public object booking { get; set; }

            [JsonProperty("bus-name")]
            public string BusName { get; set; }
            public Policy policy { get; set; }
            public List<string> features { get; set; } 
            public object description { get; set; }
            public object available { get; set; }
        }

        public class Feature
        {
            public int id { get; set; }
            public int? priority { get; set; }
            public string name { get; set; }
            public object description { get; set; }

            [JsonProperty("is-promoted")]
            public bool IsPromoted { get; set; }

            [JsonProperty("back-color")]
            public object BackColor { get; set; }

            [JsonProperty("fore-color")]
            public object ForeColor { get; set; }
        }

        
        public int id { get; set; }

        [JsonProperty("partner-id")]
        public int? PartnerId { get; set; }

        [JsonProperty("partner-name")]
        public string PartnerName { get; set; }

        [JsonProperty("route-id")]
        public int? RouteId { get; set; }

        [JsonProperty("bus-type")]
        public string BusType { get; set; }

        [JsonProperty("bus-type-name")]
        public string BusTypeName { get; set; }

        [JsonProperty("total-seats")]
        public int? TotalSeats { get; set; }

        [JsonProperty("available-seats")]
        public int? AvailableSeats { get; set; }
        public Journey journey { get; set; }
        public List<Feature> features { get; set; }  

        [JsonProperty("origin-location")]
        public string OriginLocation { get; set; }

        [JsonProperty("destination-location")]
        public string DestinationLocation { get; set; }

        [JsonProperty("is-active")]
        public bool IsActive { get; set; }

        [JsonProperty("origin-location-id")]
        public int? OriginLocationId { get; set; }

        [JsonProperty("destination-location-id")]
        public int? DestinationLocationId { get; set; }

        [JsonProperty("is-promoted")]
        public bool IsPromoted { get; set; }

        [JsonProperty("cancellation-offset")]
        public int? CancellationOffset { get; set; }

        [JsonProperty("has-bus-shuttle")]
        public bool HasBusShuttle { get; set; }

        [JsonProperty("disable-sales-without-gov-id")]
        public bool DisableSalesWithoutGovId { get; set; }

        [JsonProperty("display-offset")]
        public string DisplayOffset { get; set; }

        [JsonProperty("partner-rating")]
        public double? PartnerRating { get; set; }

        [JsonProperty("has-dynamic-pricing")]
        public bool HasDynamicPricing { get; set; }

        [JsonProperty("disable-sales-without-hes-code")]
        public bool DisableSalesWithoutHesCode { get; set; }

        [JsonProperty("disable-single-seat-selection")]
        public bool DisableSingleSeatSelection { get; set; }

        [JsonProperty("change-offset")]
        public int? ChangeOffset { get; set; }
        public int? rank { get; set; }

        [JsonProperty("display-coupon-code-input")]
        public bool DisplayCouponCodeInput { get; set; }

        [JsonProperty("disable-sales-without-date-of-birth")]
        public bool DisableSalesWithoutDateOfBirth { get; set; }
        public string status { get; set; }

        public object message { get; set; }

        [JsonProperty("user-message")]
        public object UserMessage { get; set; }

        [JsonProperty("api-request-id")]
        public object ApiRequestId { get; set; }
        public string controller { get; set; }

        [JsonProperty("client-request-id")]
        public object ClientRequestId { get; set; }

    }
}
