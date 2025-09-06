using Booket.BuildingBlocks.Infrastructure;
using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Booket.Modules.UserManagement.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T>(
        ICommandHandler<T> decorated,
        IUnitOfWork unitOfWork,
        UserManagementContext userAccessContext)
        : ICommandHandler<T>
        where T : ICommand
    {
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            await decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand = await userAccessContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}