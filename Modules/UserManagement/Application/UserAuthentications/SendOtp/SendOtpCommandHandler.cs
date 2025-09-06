using System.Security.Cryptography;
using Booket.BuildingBlocks.Application.Cache;
using Booket.BuildingBlocks.Application.Sms;
using Booket.Modules.UserManagement.Application.Configuration.Commands;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.SendOtp;

public class SendOtpCommandHandler(
    ICacheService cacheService,
    ISmsSender smsSender) : ICommandHandler<SendOtpCommand>
{
    public async Task Handle(SendOtpCommand command, CancellationToken cancellationToken)
    {
        var otp = RandomNumberGenerator.GetInt32(1000, 9999).ToString();

        var cacheKey = $"otp:{command.PhoneNumber}";
        await cacheService.SetAsync(
            cacheKey,
            otp,
            TimeSpan.FromMinutes(5),
            cancellationToken);

        var smsMessage = new SmsMessage(
            command.PhoneNumber,
            "Otp",
            otp,
            SmsTemplate.Verify);

        await smsSender.SendSms(smsMessage);
    }
}