using Booket.BuildingBlocks.Application;
using Booket.Modules.UserManagement.Application.Authorization.GetUserPermissions;
using Booket.Modules.UserManagement.Application.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Booket.API.Configuration.Authorization
{
    internal class HasPermissionAuthorizationHandler(
        IExecutionContextAccessor executionContextAccessor,
        IUserManagementModule userManagementModule)
        : AttributeAuthorizationHandler<
            HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionAuthorizationRequirement requirement,
            HasPermissionAttribute attribute)
        {
            var permissions = await userManagementModule.ExecuteQueryAsync(
                new GetUserPermissionsQuery(executionContextAccessor.UserId));

            if (!await AuthorizeAsync(attribute.Name, permissions))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(string permission, List<UserPermissionDto> permissions)
        {
#if !DEBUG
            return Task.FromResult(true);
#endif
#pragma warning disable CS0162 // Unreachable code detected
            return Task.FromResult(permissions.Any(x => x.Code == permission));
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}