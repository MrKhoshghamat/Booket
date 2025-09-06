using Booket.BuildingBlocks.Application.Outbox;

namespace Booket.Modules.UserManagement.Infrastructure.Outbox
{
    public class OutboxAccessor(UserManagementContext userAccessContext) : IOutbox
    {
        public void Add(OutboxMessage message)
        {
            userAccessContext.OutboxMessages.Add(message);
        }

        public Task Save()
        {
            return Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
        }
    }
}