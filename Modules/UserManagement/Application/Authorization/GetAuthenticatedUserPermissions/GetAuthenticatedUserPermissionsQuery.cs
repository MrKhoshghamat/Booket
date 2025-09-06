using Booket.Modules.UserManagement.Application.Authorization.GetUserPermissions;
using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.Authorization.GetAuthenticatedUserPermissions
{
    public class GetAuthenticatedUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
    {
    }
}