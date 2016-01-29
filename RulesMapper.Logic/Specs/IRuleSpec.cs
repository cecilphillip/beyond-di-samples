using Newtonsoft.Json.Linq;
using RulesMapper.Core;
using System.Threading.Tasks;

namespace RulesMapper.Logic.Specs
{
    public interface IRuleSpec
    {
        Task<bool> IsSatisfiedBy(Event instance);
    }
}
