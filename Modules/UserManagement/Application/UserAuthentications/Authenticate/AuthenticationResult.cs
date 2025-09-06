namespace Booket.Modules.UserManagement.Application.UserAuthentications.Authenticate
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string authenticationError)
        {
            IsAuthenticated = false;
            AuthenticationError = authenticationError;
        }

        public AuthenticationResult(UserRegistrationDto user)
        {
            IsAuthenticated = true;
            User = user;
        }

        public bool IsAuthenticated { get; }

        public string AuthenticationError { get; }

        public UserRegistrationDto User { get; }
    }
}