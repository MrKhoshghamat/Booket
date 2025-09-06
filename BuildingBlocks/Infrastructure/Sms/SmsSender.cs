using Booket.BuildingBlocks.Application.Data;
using Booket.BuildingBlocks.Application.Sms;
using Dapper;
using Serilog;

namespace Booket.BuildingBlocks.Infrastructure.Sms;

public class SmsSender(
    ILogger logger,
    IKavenegar kavenegar)
    : ISmsSender
{
    public async Task SendSms(SmsMessage message)
    {
        await kavenegar.VerifyLookup(
            new KavenegarMessage(
                message.To,
                message.Content,
                message.Template.ToString()));

        logger.Information(
            "Sms sent. To: {To}, Subject: {Subject}, Content: {Content}, Template: {Template}.",
            message.To,
            message.Subject,
            message.Content,
            message.Template);
    }
}