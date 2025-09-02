using MediatR;

namespace Booket.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class UnitOfWorkCommandHandlerDecorator<T>(
        IRequestHandler<T> decorated,
        IUnitOfWork unitOfWork)
        : IRequestHandler<T>
        where T : IRequest
    {
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            await decorated.Handle(command, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}