namespace Booket.BuildingBlocks.Application.Sms;

public interface ISmsSender
{
    Task SendSms(SmsMessage  message);
}