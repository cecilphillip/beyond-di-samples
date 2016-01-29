using System.Threading.Tasks;
using RulesMapper.Core;

namespace RulesMapper.Logic.Handlers
{
    [RuleHandlerMetadata("DNM-HANDLER-102", "Do nothing", "I'm sure you can guess what this does.")]
    public class EmptyHandler : IRuleHandler
    {
        public Task Execute(Event state)
        {
            return Task.CompletedTask;
        }
    }
}
