using FluentValidation;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.Authenticate
{
    internal class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber cannot be empty");
        }
    }
}