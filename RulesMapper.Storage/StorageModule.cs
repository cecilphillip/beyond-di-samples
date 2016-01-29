using Autofac;

namespace RulesMapper.Storage
{
    public class StorageModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventStore>().As<IEventStore>();
        }
    }
}
