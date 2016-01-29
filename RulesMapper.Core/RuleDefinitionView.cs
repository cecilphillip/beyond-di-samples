using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesMapper.Core
{
   public class RuleDefinitionView
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "event_type")]
        public string Event_Type { get; set; }

        [JsonProperty(PropertyName = "handler_name")]
        public string Handler_Name { get; set; }

        [JsonProperty(PropertyName = "handler_description")]
        public string Handler_Description { get; set; }

        [JsonProperty(PropertyName = "spec_name")]
        public string Spec_Name { get; set; }

        [JsonProperty(PropertyName = "spec_description")]
        public string Spec_Description { get; set; }
    }
}
