using Booket.Modules.UserManagement.Application.Contracts;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.Authenticate
{
    public class AuthenticateCommand(
        string phoneNumber,
        string otp) 
        : CommandBase<AuthenticationResult>
    {

        public string PhoneNumber { get; } = phoneNumber;
        public string Otp { get; } = otp;
    }
}