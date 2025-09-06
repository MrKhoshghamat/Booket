using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.SendOtp_Test;

public class SendOtpCommandTest(
    string phoneNumber) : CommandBase<string>
{
    public string PhoneNumber { get; } = phoneNumber;
}