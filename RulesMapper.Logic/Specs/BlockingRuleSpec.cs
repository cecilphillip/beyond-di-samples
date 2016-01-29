using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RulesMapper.Core;

namespace RulesMapper.Logic.Specs
{
    [RuleSpecMetadata("DNM-SPEC-102","Blocking","This specification always returns false.")]
    public class BlockingRuleSpec : IRuleSpec
    {
           public Task<bool> IsSatisfiedBy(Event instance)
        {
            return Task.FromResult<bool>(false);
        }
    }
}
