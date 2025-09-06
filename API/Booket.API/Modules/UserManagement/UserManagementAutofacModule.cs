using Autofac;
using Booket.Modules.UserManagement.Application.Contracts;
using Booket.Modules.UserManagement.Infrastructure;

namespace Booket.API.Modules.UserManagement;

public class UserManagementAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserManagementModule>()
            .As<IUserManagementModule>()
            .InstancePerLifetimeScope();
    }
}