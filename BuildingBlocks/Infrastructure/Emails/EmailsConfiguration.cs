namespace Booket.BuildingBlocks.Infrastructure.Emails
{
    public class EmailsConfiguration(string fromEmail)
    {
        public string FromEmail { get; } = fromEmail;
    }
}