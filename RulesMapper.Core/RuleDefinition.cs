using Newtonsoft.Json;
using System;

namespace RulesMapper.Core
{
    public class RuleDefinition
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "event_type")]
        public string Event_Type { get; set; }

        [JsonProperty(PropertyName = "handler_code")]
        public string Handler_Code { get; set; }

        [JsonProperty(PropertyName = "spec_code")]
        public string Spec_Code { get; set; }       
    }
}
