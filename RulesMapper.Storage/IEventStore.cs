using Newtonsoft.Json.Linq;
using RulesMapper.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesMapper.Storage
{
    public interface IEventStore
    {
        void AddEventItem(JObject state);

        void AddEventItem(Event state);

        IEnumerable<Event> GetEvents();
        IEnumerable<RuleDefinition> GetRuleDefinitions();
      
        void AddRuleDefinition(RuleDefinition def);
    }
}
