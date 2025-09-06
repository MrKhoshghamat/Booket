using Booket.Modules.UserManagement.Application.Contracts;
using Booket.Modules.UserManagement.Application.UserAuthentications.Authenticate;
using Booket.Modules.UserManagement.Application.UserAuthentications.RegisterUser;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace Booket.Modules.UserManagement.Infrastructure.IdentityServer;

internal class OtpGrantValidator(
    IUserManagementModule userManagementModule) : IExtensionGrantValidator
{
    public string GrantType => "otp";

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        try
        {
            var phoneNumber = context.Request.Raw.Get("phoneNumber");
            var otp = context.Request.Raw.Get("otp");

            var authenticationResult = await userManagementModule.ExecuteCommandAsync(
                new AuthenticateCommand(phoneNumber, otp));

            if (!authenticationResult.IsAuthenticated &&
                authenticationResult.AuthenticationError == "User not found")
            {
                await userManagementModule.ExecuteCommandAsync(
                    new RegisterUserCommand(phoneNumber));

                authenticationResult = await userManagementModule.ExecuteCommandAsync(
                    new AuthenticateCommand(phoneNumber, otp));
            }

            if (!authenticationResult.IsAuthenticated)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    authenticationResult.AuthenticationError);

                return;
            }

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}