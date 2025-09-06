using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.SendOtp;

public class SendOtpCommand(
    string phoneNumber) : CommandBase
{
    public string PhoneNumber { get; } = phoneNumber;
}