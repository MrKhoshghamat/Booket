using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.UserRegistrations;

public class UserRegistrationStatus(string value) : ValueObject
{
    public static UserRegistrationStatus WaitingForRegistration =>
        new UserRegistrationStatus(nameof(WaitingForRegistration));

    public static UserRegistrationStatus Registered =>
        new UserRegistrationStatus(nameof(Registered));

    public string Value { get; private set; } = value;
}