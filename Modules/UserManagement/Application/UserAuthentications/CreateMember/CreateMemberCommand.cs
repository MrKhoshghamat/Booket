using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.CreateMember;

public class CreateMemberCommand(
    Guid userRegistrationId,
    string email,
    string firstName,
    string lastName) : CommandBase<Guid>
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
}