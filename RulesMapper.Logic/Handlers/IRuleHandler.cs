using RulesMapper.Core;
using System.Threading.Tasks;

namespace RulesMapper.Logic.Handlers
{
    public interface IRuleHandler
    {
        Task Execute(Event state);
    }
}
