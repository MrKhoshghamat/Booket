using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Booket.Modules.UserManagement.Domain.Users;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.CreateMember;

public class CreateMemberCommandHandler(
    IUserRepository userRepository) : ICommandHandler<CreateMemberCommand, Guid>
{
    public async Task<Guid> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var user = User.CreateMember(
            new UserRegistrationId(command.UserRegistrationId),
            command.Email,
            command.FirstName,
            command.LastName);

        await userRepository.AddAsync(user);

        return user.Id.Value;
    }
}