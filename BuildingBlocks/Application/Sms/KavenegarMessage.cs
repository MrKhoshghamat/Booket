namespace Booket.BuildingBlocks.Application.Sms;

public readonly struct KavenegarMessage(
    string receptor,
    string token,
    string template)
{
    public string Receptor { get; } = receptor;
    public string Token { get; } = token;
    public string Template { get; } = template;
}