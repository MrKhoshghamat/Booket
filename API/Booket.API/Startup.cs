using Autofac;
using Autofac.Extensions.DependencyInjection;
using Booket.API.Configuration.Authorization;
using Booket.API.Configuration.ExecutionContext;
using Booket.API.Configuration.Extensions;
using Booket.API.Configuration.Validation;
using Booket.API.Modules.UserManagement;
using Booket.BuildingBlocks.Application;
using Booket.BuildingBlocks.Application.Cache;
using Booket.BuildingBlocks.Domain;
using Booket.BuildingBlocks.Infrastructure.Sms;
using Booket.Modules.UserManagement.Infrastructure.Configuration;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Identity;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace Booket.API
{
    public class Startup
    {
        private const string BooketConnectionString = "BooketConnectionString";

        private static ILogger _logger;
        private static ILogger _loggerForApi;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            ConfigureLogger();

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables("Booket_")
                .Build();

            _loggerForApi.Information("Connection string:" + _configuration.GetConnectionString(BooketConnectionString));

            AuthorizationChecker.CheckAllEndpoints();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerDocumentation();

            services.ConfigureIdentityService();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
                    policyBuilder.AddAuthenticationSchemes("Bearer");
                });
            });

            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new UserManagementAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            InitializeModules(container);

            app.UseMiddleware<CorrelationMiddleware>();

            app.UseSwaggerDocumentation();

            app.AddIdentityService();

            if (env.IsDevelopment())
            {
                app.UseProblemDetails();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);
            var kavenegarConfiguration = new KavenegarConfiguration(_configuration["Kavenegar:ApiKey"]);
            UserManagementStartup.Initialize(
                _configuration.GetConnectionString(BooketConnectionString),
                kavenegarConfiguration,
                executionContextAccessor,
                _logger,
                _configuration["Security:TextEncryptionKey"],
                null,
                null);
        }
    }
}