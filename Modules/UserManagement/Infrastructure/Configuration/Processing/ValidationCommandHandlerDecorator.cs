using Booket.BuildingBlocks.Application;
using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Booket.Modules.UserManagement.Application.Contracts;
using FluentValidation;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerDecorator<T>(
        IList<IValidator<T>> validators,
        ICommandHandler<T> decorated)
        : ICommandHandler<T>
        where T : ICommand
    {
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            var errors = validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
            }

            await decorated.Handle(command, cancellationToken);
        }
    }
}