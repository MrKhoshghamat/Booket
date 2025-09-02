namespace Booket.BuildingBlocks.Infrastructure.Sms;

public class KavenegarConfiguration(string apiKey)
{
    public string ApiKey { get; } = apiKey;
}