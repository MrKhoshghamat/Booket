using Autofac;
using Booket.Modules.UserManagement.Application.Contracts;
using Booket.Modules.UserManagement.Infrastructure.Configuration;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Processing;
using MediatR;

namespace Booket.Modules.UserManagement.Infrastructure
{
    public class UserManagementModule : IUserManagementModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = UserManagementCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}