using Autofac;
using Booket.Modules.UserManagement.Application.UserAuthentications;
using Booket.Modules.UserManagement.Domain.UserRegistrations;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Domain
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
        }
    }
}