using Autofac;
using Booket.BuildingBlocks.Infrastructure.EventBus;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.EventsBus
{
    internal class EventsBusModule(IEventsBus eventsBus)
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (eventsBus != null)
            {
                builder.RegisterInstance(eventsBus).SingleInstance();
            }
            else
            {
                builder.RegisterType<InMemoryEventBusClient>()
                    .As<IEventsBus>()
                    .SingleInstance();
            }
        }
    }
}