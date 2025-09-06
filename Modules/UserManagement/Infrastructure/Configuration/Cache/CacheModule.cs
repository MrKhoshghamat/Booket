using Autofac;
using Booket.BuildingBlocks.Application.Cache;
using Booket.BuildingBlocks.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Cache;

public class CacheModule(
    ICacheService cacheService)
    : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(Options.Create(new MemoryDistributedCacheOptions()))
            .As<IOptions<MemoryDistributedCacheOptions>>()
            .SingleInstance();

        builder.RegisterType<MemoryDistributedCache>()
            .As<IDistributedCache>()
            .SingleInstance();

        if (cacheService != null)
        {
            builder.RegisterInstance(cacheService);
        }
        else
        {
            builder.RegisterType<CacheService>()
                .As<ICacheService>()
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IDistributedCache),
                    (pi, ctx) => ctx.Resolve<IDistributedCache>()
                )
                .InstancePerLifetimeScope();
        }
    }
}