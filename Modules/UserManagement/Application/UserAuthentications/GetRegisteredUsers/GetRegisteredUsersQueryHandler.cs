using Booket.BuildingBlocks.Application.Data;
using Booket.Modules.UserManagement.Application.Configuration.Queries;
using Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUser;
using Dapper;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUsers;

public class GetRegisteredUsersQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetRegisteredUsersQuery, List<UserRegistrationDto>>
{
    public async Task<List<UserRegistrationDto>> Handle(GetRegisteredUsersQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();
        const string sql = $"""
                             SELECT 
                                [UserRegistration].[Id] as [{nameof(UserRegistrationDto.Id)}],
                                [UserRegistration].[PhoneNumber] as [{nameof(UserRegistrationDto.PhoneNumber)}]
                                [UserRegistration].[RegisterDate] as [{nameof(UserRegistrationDto.RegisterDate)}]
                                [UserRegistration].[StatusCode] as [{nameof(UserRegistrationDto.StatusCode)}]
                            FROM [registrations].[UserRegistrations] AS [UserRegistration] 
                            """;

        var userRegistrations = 
            await connection.QueryAsync<UserRegistrationDto>(sql);

        return userRegistrations.AsList();
    }
}