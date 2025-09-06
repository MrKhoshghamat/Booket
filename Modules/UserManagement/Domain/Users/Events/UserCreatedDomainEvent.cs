using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.Users.Events;

public class UserCreatedDomainEvent(
    UserId userId,
    UserRole role) : DomainEventBase
{
    public UserId UserId { get; } = userId;
    public UserRole Role { get; } = role;
}