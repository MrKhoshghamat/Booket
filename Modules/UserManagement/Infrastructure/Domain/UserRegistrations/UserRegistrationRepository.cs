using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Booket.Modules.UserManagement.Infrastructure.Domain.UserRegistrations
{
    public class UserRegistrationRepository(UserManagementContext context)
        : IUserRegistrationRepository
    {
        public async Task AddAsync(UserRegistration userRegistration)
        {
            await context.AddAsync(userRegistration);
        }

        public async Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId)
        {
            return await context.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}