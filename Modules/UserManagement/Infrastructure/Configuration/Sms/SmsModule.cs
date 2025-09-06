using Autofac;
using Booket.BuildingBlocks.Application.Sms;
using Booket.BuildingBlocks.Infrastructure.Sms;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Sms;

internal class SmsModule(
    KavenegarConfiguration kavenegarConfiguration)
    : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BuildingBlocks.Infrastructure.Sms.Kavenegar>()
            .As<IKavenegar>()
            .WithParameter("kavenegarConfiguration", kavenegarConfiguration)
            .InstancePerLifetimeScope();

        builder.RegisterType<SmsSender>()
            .As<ISmsSender>()
            .InstancePerLifetimeScope();
    }
}