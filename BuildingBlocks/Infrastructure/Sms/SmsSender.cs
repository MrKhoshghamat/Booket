using Booket.BuildingBlocks.Application.Data;
using Booket.BuildingBlocks.Application.Sms;
using Dapper;
using Serilog;

namespace Booket.BuildingBlocks.Infrastructure.Sms;

public class SmsSender(
    ILogger logger,
    ISqlConnectionFactory sqlConnectionFactory,
    IKavenegar kavenegar)
    : ISmsSender
{
    public async Task SendSms(SmsMessage message)
    {
        var sqlConnection = sqlConnectionFactory.GetOpenConnection();

        await kavenegar.VerifyLookup(
            new KavenegarMessage(
                message.To,
                message.Content,
                message.Template.ToString()));

        await sqlConnection.ExecuteScalarAsync(
            "INSERT INTO [app].[Sms] ([Id], [To], [Subject], [Content], [Template], [Date]) " +
            "VALUES (@Id, @From, @To, @Subject, @Content, @Template, @Date)",
            new
            {
                Id = Guid.NewGuid(),
                message.To,
                message.Subject,
                message.Content,
                message.Template,
                DateTime.UtcNow
            });

        logger.Information(
            "Sms sent. To: {To}, Subject: {Subject}, Content: {Content}, Template: {Template}.",
            message.To,
            message.Subject,
            message.Content,
            message.Template);
    }
}