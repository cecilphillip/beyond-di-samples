using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesMapper.Core
{
    public class Event
    {
        [JsonProperty(PropertyName ="id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "event_type")]
        public string Event_Type { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, string> Properties { get; set; }

        [JsonProperty(PropertyName = "date_created")]
        public DateTime Date_Created { get; set; }

        public Event()
        {
            Properties = new Dictionary<string, string>();
        }
    }
}
