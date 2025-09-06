using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.UserRegistrations.Events;

public class NewUserRegisteredDomainEvent(
    UserRegistrationId userId,
    string phoneNumber,
    DateTime registerDate) 
    : DomainEventBase
{
    public UserRegistrationId UserId { get; } = userId;
    public string PhoneNumber { get; } = phoneNumber;
    public DateTime RegisterDate { get; } = registerDate;
}