using Booket.Modules.UserManagement.Application.Contracts;
using Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUser;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUsers;

public record GetRegisteredUsersQuery : IQuery<List<UserRegistrationDto>>;
