using MediatR;

namespace Booket.Modules.UserManagement.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}