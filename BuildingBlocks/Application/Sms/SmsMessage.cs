namespace Booket.BuildingBlocks.Application.Sms;

public readonly struct SmsMessage(
    string to,
    string subject,
    string content,
    SmsTemplate template)
{
    public string To { get; } = to;
    public string Subject { get; } = subject;
    public string Content { get; } = content;
    public SmsTemplate Template { get; } = template;
}