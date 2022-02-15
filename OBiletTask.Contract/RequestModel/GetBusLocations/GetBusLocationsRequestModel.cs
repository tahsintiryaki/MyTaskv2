using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.RequestModel.GetBusLocations
{
    public class GetBusLocationsRequestModel
    {
        public string data { get; set; }

        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; }
        public DateTime date { get; set; }
        public string language { get; set; }
       
    }
    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
