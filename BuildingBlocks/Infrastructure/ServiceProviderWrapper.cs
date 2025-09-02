using Autofac;

namespace Booket.BuildingBlocks.Infrastructure
{
    public class ServiceProviderWrapper(ILifetimeScope lifeTimeScope) : IServiceProvider
    {
#nullable enable
        public object? GetService(Type serviceType) => lifeTimeScope.ResolveOptional(serviceType);
    }
}
