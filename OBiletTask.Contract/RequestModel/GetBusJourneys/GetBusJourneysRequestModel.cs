using Newtonsoft.Json;
using OBiletTask.RequestModel.GetBusLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.RequestModel.GetBusJourneys
{
    public class GetBusJourneysRequestModel
    {
     


        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; }
        public DateTime date { get; set; }
        public string language { get; set; }
        public Data data { get; set; }

        public class Data
        {
            [JsonProperty("origin-id")]
            public int OriginId { get; set; }

            [JsonProperty("destination-id")]
            public int DestinationId { get; set; }

            [JsonProperty("departure-date")]
            public DateTime DepartureDate { get; set; }

            public String OriginLocation { get; set; }
            public String DestinationLocation { get; set; }
        }

    }
}
