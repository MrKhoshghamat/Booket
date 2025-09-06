using Booket.BuildingBlocks.Domain;
using Booket.Modules.UserManagement.Domain.UserRegistrations.Events;
using Booket.Modules.UserManagement.Domain.UserRegistrations.Rules;

namespace Booket.Modules.UserManagement.Domain.UserRegistrations;

public class UserRegistration : Entity, IAggregateRoot
{
    public UserRegistrationId Id { get; private set; }
    public string PhoneNumber { get; private set; }

    private DateTime _registerDate;
    private UserRegistrationStatus _status;

    public static UserRegistration RegisterNewUser(
        string phoneNumber,
        IUsersCounter usersCounter)
    {
        return new UserRegistration(
            phoneNumber,
            usersCounter);
    }

    private UserRegistration()
    {
        _status = UserRegistrationStatus.WaitingForRegistration;
    }

    private UserRegistration(
        string phoneNumber,
        IUsersCounter usersCounter)
    {
        CheckRule(new UserPhoneNumberMustBeUniqueRule(usersCounter, phoneNumber));

        Id = new UserRegistrationId(Guid.NewGuid());
        PhoneNumber = phoneNumber;
        _registerDate = DateTime.UtcNow;
        _status = UserRegistrationStatus.Registered;

        AddDomainEvent(new NewUserRegisteredDomainEvent(
            Id,
            PhoneNumber,
            _registerDate));
    }

}