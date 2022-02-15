using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.RequestModel.GetSessions
{
    public class GetSession
    {

        public int type { get; set; }
        public Connection connection { get; set; }
        public Application application { get; set; }
        public Browser browser { get; set; }




        public class Browser
        {
            public string name { get; set; }
            public string version { get; set; }
        }

        

        public class Connection
        {
            [JsonProperty("ip-address")]
            public string IpAddress { get; set; }
            public string port { get; set; }
        }

        public class Application
        {
            public string version { get; set; }

            [JsonProperty("equipment-id")]
            public string EquipmentId { get; set; }
        }





    }


}
