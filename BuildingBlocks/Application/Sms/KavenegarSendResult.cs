namespace Booket.BuildingBlocks.Application.Sms;

public class KavenegarSendResult(
    long messageId,
    int cost,
    long date,
    string message,
    string receptor,
    string sender,
    int status,
    string statusText)
{
    public long MessageId { get; } = messageId;
    public int Cost { get; } = cost;
    public long Date { get; } = date;
    public string Message { get; } = message;
    public string Receptor { get; } = receptor;
    public string Sender { get; } = sender;
    public int Status { get; } = status;
    public string StatusText { get; } = statusText;
}