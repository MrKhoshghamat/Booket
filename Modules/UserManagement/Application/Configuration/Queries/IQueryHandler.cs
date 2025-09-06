using Booket.Modules.UserManagement.Application.Contracts;
using MediatR;

namespace Booket.Modules.UserManagement.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}