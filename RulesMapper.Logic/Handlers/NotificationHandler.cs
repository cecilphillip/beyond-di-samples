using System;
using System.Threading.Tasks;
using RulesMapper.Core;
using Autofac.Extras.AttributeMetadata;
using Microsoft.AspNet.SignalR;

namespace RulesMapper.Logic.Handlers
{
    [RuleHandlerMetadata("DNM-HANDLER-101","Notify Client", "This sends a notification via SignalR to the respective client")]
    public class NotificationHandler : IRuleHandler
    {
        private IHubContext _hubContext;

        public NotificationHandler([WithKey("EventsHub")]IHubContext context)
        {
            _hubContext = context;
        }
        public  Task Execute(Event state)
        {
            string connectionId;
            if (state.Properties.TryGetValue("signalr_connection_id", out connectionId))
            {
                return _hubContext.Clients.Client(connectionId).notify("Hi there from the NotificationHandler");
            }
            else {
                return _hubContext.Clients.All.notify("Hi EVERYBODY from the NotificationHandler");
            }
        }
    }
}
