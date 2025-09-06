namespace Booket.Modules.UserManagement.Domain.UserRegistrations;

public interface IUsersCounter
{
    int CountUsersWithPhoneNumber(string phoneNumber);
}