using Autofac;
using Microsoft.AspNet.SignalR;
using RulesMapper.Core;
using RulesMapper.Logic;
using RulesMapper.Web.App_Start;
using System.Web.Hosting;

namespace RulesMapper.Web.Hubs
{
    public class EventsHub: Hub
    {
        public virtual void Evaluate(Event state)
        {
            state.Properties["signalr_connection_id"] = Context.ConnectionId;

            HostingEnvironment.QueueBackgroundWorkItem(ct =>
            {
                using (var nestedScope = Bootstrapper.Container.BeginLifetimeScope())
                {
                    var nestedEngine = nestedScope.Resolve<IRulesEngine>();
                    return nestedEngine.ExecuteAsync(state);
                }
            });  
        }
    }
}