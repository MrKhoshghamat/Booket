using Booket.Modules.UserManagement.Infrastructure.IdentityServer;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Identity;

public static class IdentityConfiguration
{
    public static IServiceCollection ConfigureIdentityService(this IServiceCollection services)
    {
        services.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryApiResources(IdentityServerConfig.GetApis())
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryPersistedGrants()
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();

        //services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
        services.AddTransient<IExtensionGrantValidator, OtpGrantValidator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.Authority = "http://localhost:5000";
                x.Audience = "booketAPI";
                x.RequireHttpsMetadata = false;
            });

        return services;
    }

    public static IApplicationBuilder AddIdentityService(this IApplicationBuilder app)
    {
        app.UseIdentityServer();
        return app;
    }
}
