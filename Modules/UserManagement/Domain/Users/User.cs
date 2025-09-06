using Booket.BuildingBlocks.Domain;
using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Booket.Modules.UserManagement.Domain.Users.Events;

namespace Booket.Modules.UserManagement.Domain.Users;

public class User : Entity, IAggregateRoot
{
    public UserId Id { get; }

    public UserRegistrationId UserRegistrationId { get; private set; }
    private string _email;
    private string _firstName;
    private string _lastName;
    private string _name;
    private List<UserRole> _roles;

    private User()
    {

    }

    public static User CreateAdmin(
        UserRegistrationId userRegistrationId,
        string email,
        string firstName,
        string lastName)
    {
        return new User(
            Guid.NewGuid(),
            userRegistrationId,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            UserRole.Admin);
    }

    public static User CreateMember(
        UserRegistrationId userRegistrationId,
        string email,
        string firstName,
        string lastName)
    {
        return new User(
            Guid.NewGuid(),
            userRegistrationId,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            UserRole.Member);
    }

    public static User CreatePublisher(
        UserRegistrationId userRegistrationId,
        string email,
        string firstName,
        string lastName)
    {
        return new User(
            Guid.NewGuid(),
            userRegistrationId,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            UserRole.Publisher);
    }

    private User(
        Guid id,
        UserRegistrationId userRegistrationId,
        string email,
        string firstName,
        string lastName,
        string name,
        UserRole role)
    {
        Id = new UserId(id);
        UserRegistrationId = userRegistrationId;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = name;

        _roles = [role];

        AddDomainEvent(new UserCreatedDomainEvent(
            Id,
            role));
    }

}