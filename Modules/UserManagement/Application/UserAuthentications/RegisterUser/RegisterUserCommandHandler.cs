using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Booket.Modules.UserManagement.Domain.UserRegistrations;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRegistrationRepository userRegistrationRepository,
    IUsersCounter usersCounter)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userRegistration = UserRegistration.RegisterNewUser(
                command.PhoneNumber,
                usersCounter);

            await userRegistrationRepository.AddAsync(userRegistration);

            return userRegistration.Id.Value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}