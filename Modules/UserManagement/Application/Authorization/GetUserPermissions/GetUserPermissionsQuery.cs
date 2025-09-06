using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.Authorization.GetUserPermissions
{
    public class GetUserPermissionsQuery(Guid userId)
        : QueryBase<List<UserPermissionDto>>
    {
        public Guid UserId { get; } = userId;
    }
}