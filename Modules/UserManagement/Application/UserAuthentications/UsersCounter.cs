using Booket.BuildingBlocks.Application.Data;
using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Dapper;

namespace Booket.Modules.UserManagement.Application.UserAuthentications;

public class UsersCounter(
    ISqlConnectionFactory sqlConnectionFactory) 
    : IUsersCounter
{
    public int CountUsersWithPhoneNumber(string phoneNumber)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql = """
                           SELECT COUNT(*) 
                           FROM [registrations].[UserRegistrations] AS [UserRegistration]
                           WHERE [UserRegistration].[PhoneNumber] = @PhoneNumber
                           """;

        return connection.QuerySingle<int>(
            sql,
            new { phoneNumber });
    }
}