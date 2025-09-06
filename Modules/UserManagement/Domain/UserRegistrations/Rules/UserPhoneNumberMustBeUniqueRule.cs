using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.UserRegistrations.Rules;

public class UserPhoneNumberMustBeUniqueRule : IBusinessRule
{
    private readonly IUsersCounter _usersCounter;
    private readonly string _phoneNumber;

    internal UserPhoneNumberMustBeUniqueRule(IUsersCounter usersCounter, string phoneNumber)
    {
        _usersCounter = usersCounter;
        _phoneNumber = phoneNumber;
    }

    public bool IsBroken() => _usersCounter.CountUsersWithPhoneNumber(_phoneNumber) > 0;

    public string Message => "User PhoneNumber must be unique";
}