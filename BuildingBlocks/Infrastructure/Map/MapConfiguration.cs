namespace Booket.BuildingBlocks.Infrastructure.Map;

public class MapConfiguration(
    string apiKey,
    string clientName,
    string baseUel,
    Dictionary<string, string> urls)
{
    public string ApiKey { get; } = apiKey;
    public string ClientName { get; } = clientName;
    public string BaseUel { get; } = baseUel;
    public Dictionary<string, string> Urls { get; set; } = urls;
}