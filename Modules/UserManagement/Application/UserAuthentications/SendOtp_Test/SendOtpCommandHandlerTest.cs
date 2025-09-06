using Booket.BuildingBlocks.Application.Cache;
using Booket.Modules.UserManagement.Application.Configuration.Commands;
using System.Security.Cryptography;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.SendOtp_Test;

public class SendOtpCommandHandlerTest(
    ICacheService cacheService) : ICommandHandler<SendOtpCommandTest, string>
{
    public async Task<string> Handle(SendOtpCommandTest command, CancellationToken cancellationToken)
    {
        var otp = RandomNumberGenerator.GetInt32(1000, 9999).ToString();

        var cacheKey = $"otp:{command.PhoneNumber}";
        await cacheService.SetAsync(
            cacheKey,
            otp,
            TimeSpan.FromMinutes(5),
            cancellationToken);

        return otp;
    }
}