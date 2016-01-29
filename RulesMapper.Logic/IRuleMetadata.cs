using Newtonsoft.Json;

namespace RulesMapper.Logic.Handlers
{
    public interface IRuleMetadata
    {
        [JsonProperty(PropertyName = "name")]
        string Name { get; }

        [JsonProperty(PropertyName = "code")]
        string Code { get; }

        [JsonProperty(PropertyName = "description")]
        string Description { get; }
    }
}