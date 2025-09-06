using Booket.Modules.UserManagement.Application.Contracts;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace Booket.Modules.UserManagement.Infrastructure.IdentityServer
{
    internal class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims.Where(x => x.Type == CustomClaimTypes.Roles).ToList());
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == CustomClaimTypes.PhoneNumber));
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(context.IsActive);
        }
    }
}