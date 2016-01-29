using Newtonsoft.Json.Linq;
using RulesMapper.Core;
using System.Threading.Tasks;

namespace RulesMapper.Logic.Specs
{
    [RuleSpecMetadata("DNM-SPEC-101", "Pass Through", "This specification always evaluates as true.")]
    public class PassThroughSpec : IRuleSpec
    {
        public virtual Task<bool> IsSatisfiedBy(Event instance)
        {
            return Task.FromResult(true);
        }
    }
}
