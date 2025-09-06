using Booket.BuildingBlocks.Application;
using Booket.BuildingBlocks.Application.Data;
using Booket.Modules.UserManagement.Application.Authorization.GetUserPermissions;
using Booket.Modules.UserManagement.Application.Configuration.Queries;
using Dapper;

namespace Booket.Modules.UserManagement.Application.Authorization.GetAuthenticatedUserPermissions
{
    internal class GetAuthenticatedUserPermissionsQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IExecutionContextAccessor executionContextAccessor)
        : IQueryHandler<GetAuthenticatedUserPermissionsQuery, List<UserPermissionDto>>
    {
        public async Task<List<UserPermissionDto>> Handle(GetAuthenticatedUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            if (!executionContextAccessor.IsAvailable)
            {
                return [];
            }

            var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = $""" 
                               SELECT [UserPermission].[PermissionCode] AS [{nameof(UserPermissionDto.Code)}]
                               FROM [users].[UserPermissions] AS [UserPermission] 
                               WHERE [UserPermission].UserId = @UserId
                               """;

            var permissions = await connection.QueryAsync<UserPermissionDto>(
                sql,
                new { executionContextAccessor.UserId });

            return permissions.AsList();
        }
    }
}