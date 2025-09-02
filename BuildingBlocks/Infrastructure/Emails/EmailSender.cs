using Booket.BuildingBlocks.Application.Data;
using Booket.BuildingBlocks.Application.Emails;
using Dapper;
using Serilog;

namespace Booket.BuildingBlocks.Infrastructure.Emails
{
    public class EmailSender(
        ILogger logger,
        EmailsConfiguration configuration,
        ISqlConnectionFactory sqlConnectionFactory)
        : IEmailSender
    {
        public async Task SendEmail(EmailMessage message)
        {
            var sqlConnection = sqlConnectionFactory.GetOpenConnection();

            await sqlConnection.ExecuteScalarAsync(
                "INSERT INTO [app].[Emails] ([Id], [From], [To], [Subject], [Content], [Date]) " +
                "VALUES (@Id, @From, @To, @Subject, @Content, @Date) ",
                new
                {
                    Id = Guid.NewGuid(),
                    From = configuration.FromEmail,
                    message.To,
                    message.Subject,
                    message.Content,
                    Date = DateTime.UtcNow
                });

            logger.Information(
                "Email sent. From: {From}, To: {To}, Subject: {Subject}, Content: {Content}.",
                configuration.FromEmail,
                message.To,
                message.Subject,
                message.Content);
        }
    }
}