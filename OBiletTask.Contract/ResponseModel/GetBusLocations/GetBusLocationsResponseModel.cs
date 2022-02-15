using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.ResponseModel.GetBusLocations
{
    public class GetBusLocationsResponseModel
    {
 
        public int id { get; set; }

        [JsonProperty("parent-id")]
        public int ParentId { get; set; }
        public string type { get; set; }
        public string name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation GeoLocation { get; set; }
        public int zoom { get; set; }

        [JsonProperty("tz-code")]
        public string TzCode { get; set; }

        [JsonProperty("weather-code")]
        public object WeatherCode { get; set; }
        public object rank { get; set; }

        [JsonProperty("reference-code")]
        public object ReferenceCode { get; set; }
        public object keywords { get; set; }
    }

    public class GeoLocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int zoom { get; set; }
    }


}
