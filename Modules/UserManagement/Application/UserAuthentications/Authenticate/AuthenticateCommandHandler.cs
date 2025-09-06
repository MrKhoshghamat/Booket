using System.Security.Claims;
using Booket.BuildingBlocks.Application.Cache;
using Booket.BuildingBlocks.Application.Data;
using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Booket.Modules.UserManagement.Application.Contracts;
using Dapper;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.Authenticate
{
    internal class AuthenticateCommandHandler(
        ICacheService cacheService,
        ISqlConnectionFactory sqlConnectionFactory) 
        : ICommandHandler<AuthenticateCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = $"otp:{command.PhoneNumber}";
                var cachedOtp = await cacheService.GetAsync<string>(cacheKey, cancellationToken);
                if (string.IsNullOrEmpty(cachedOtp)) return new AuthenticationResult("Otp expired");

                var isOtpValid = cachedOtp == command.Otp;
                if (!isOtpValid) return new AuthenticationResult("Invalid Otp");

                var connection = sqlConnectionFactory.GetOpenConnection();

                const string sql = $"""
                                     SELECT 
                                        [UserRegistration].[Id] as [{nameof(UserRegistrationDto.Id)}],
                                        [UserRegistration].[PhoneNumber] as [{nameof(UserRegistrationDto.PhoneNumber)}]
                                    FROM [registrations].[UserRegistrations] AS [UserRegistration] 
                                    WHERE [UserRegistration].[PhoneNumber] = @PhoneNumber
                                    """;

                var userRegistration = await connection.QuerySingleOrDefaultAsync<UserRegistrationDto>(
                    sql,
                    new
                    {
                        command.PhoneNumber,
                    });

                if (userRegistration == null)
                {
                    return new AuthenticationResult("User not found");
                }

                userRegistration.Claims =
                [
                    new Claim(CustomClaimTypes.PhoneNumber, userRegistration.PhoneNumber),
                ];

                return new AuthenticationResult(userRegistration);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}