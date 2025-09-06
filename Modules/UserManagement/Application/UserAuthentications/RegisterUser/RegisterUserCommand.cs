using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.RegisterUser;

public class RegisterUserCommand(
    string phoneNumber) 
    : CommandBase<Guid>
{
    public string PhoneNumber { get; } = phoneNumber;
}