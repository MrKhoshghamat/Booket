using Booket.BuildingBlocks.Application.Data;
using Booket.Modules.UserManagement.Application.Configuration.Queries;
using Dapper;

namespace Booket.Modules.UserManagement.Application.Authorization.GetUserPermissions
{
    internal class GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        : IQueryHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
    {
        public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT [UserPermission].[PermissionCode] AS [{nameof(UserPermissionDto.Code)}]
                                FROM [users].[UserPermissions] AS [UserPermission] 
                                WHERE [UserPermission].UserId = @UserId
                                """;
            var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new { request.UserId });

            return permissions.AsList();
        }
    }
}