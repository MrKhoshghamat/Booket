using Booket.Modules.UserManagement.Domain.Users;

namespace Booket.Modules.UserManagement.Infrastructure.Domain.Users
{
    public class UserRepository(UserManagementContext userAccessContext)
        : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await userAccessContext.Users.AddAsync(user);
        }
    }
}