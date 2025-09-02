namespace Booket.BuildingBlocks.Application.Sms;

public interface IKavenegar
{
    Task<KavenegarSendResult> VerifyLookup(KavenegarMessage message);
}