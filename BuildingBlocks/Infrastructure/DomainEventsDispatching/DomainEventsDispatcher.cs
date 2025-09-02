using Autofac;
using Autofac.Core;
using Booket.BuildingBlocks.Application.Events;
using Booket.BuildingBlocks.Application.Outbox;
using Booket.BuildingBlocks.Domain;
using Booket.BuildingBlocks.Infrastructure.Serialization;
using MediatR;
using Newtonsoft.Json;

namespace Booket.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsDispatcher(
        IMediator mediator,
        ILifetimeScope scope,
        IOutbox outbox,
        IDomainEventsAccessor domainEventsProvider,
        IDomainNotificationsMapper domainNotificationsMapper)
        : IDomainEventsDispatcher
    {
        public async Task DispatchEventsAsync()
        {
            var domainEvents = domainEventsProvider.GetAllDomainEvents();

            List<IDomainEventNotification<IDomainEvent>> domainEventNotifications = [];
            domainEventNotifications.AddRange(from domainEvent in domainEvents let domainEvenNotificationType = typeof(IDomainEventNotification<>) let domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType()) select scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter> { new NamedParameter("domainEvent", domainEvent), new NamedParameter("id", domainEvent.Id) }) into domainNotification where domainNotification != null select domainNotification as IDomainEventNotification<IDomainEvent>);

            domainEventsProvider.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

            foreach (var outboxMessage in from domainEventNotification in domainEventNotifications
                                          let type = domainNotificationsMapper.GetName(domainEventNotification.GetType())
                                          let data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                                          {
                                              ContractResolver = new AllPropertiesContractResolver()
                                          })
                                          select new OutboxMessage(
                                              domainEventNotification.Id,
                                              domainEventNotification.DomainEvent.OccurredOn,
                                              type,
                                              data))
            {
                outbox.Add(outboxMessage);
            }
        }
    }
}