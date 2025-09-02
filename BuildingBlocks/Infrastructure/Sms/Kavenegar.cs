using Booket.BuildingBlocks.Application.Sms;
using Kavenegar;

namespace Booket.BuildingBlocks.Infrastructure.Sms;

public class Kavenegar(
    KavenegarConfiguration kavenegarConfiguration)
    : IKavenegar
{
    private readonly KavenegarApi _kavenegarApi = new(kavenegarConfiguration.ApiKey);

    public async Task<KavenegarSendResult> VerifyLookup(KavenegarMessage message)
    {
        var result = await _kavenegarApi.VerifyLookup(
            message.Receptor,
            message.Token,
            message.Template);

        return new KavenegarSendResult(
            result.Messageid,
            result.Cost,
            result.Date,
            result.Message,
            result.Receptor,
            result.Sender,
            result.Status,
            result.StatusText);
    }
}