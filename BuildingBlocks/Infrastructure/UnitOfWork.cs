using Booket.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace Booket.BuildingBlocks.Infrastructure
{
    public class UnitOfWork(
        DbContext context,
        IDomainEventsDispatcher domainEventsDispatcher)
        : IUnitOfWork
    {
        public async Task<int> CommitAsync(
            CancellationToken cancellationToken = default,
            Guid? internalCommandId = null)
        {
            await domainEventsDispatcher.DispatchEventsAsync();

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}