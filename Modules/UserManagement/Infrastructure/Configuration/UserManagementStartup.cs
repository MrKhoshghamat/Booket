using Autofac;
using Booket.BuildingBlocks.Application;
using Booket.BuildingBlocks.Application.Cache;
using Booket.BuildingBlocks.Infrastructure;
using Booket.BuildingBlocks.Infrastructure.Cache;
using Booket.BuildingBlocks.Infrastructure.Emails;
using Booket.BuildingBlocks.Infrastructure.EventBus;
using Booket.BuildingBlocks.Infrastructure.Sms;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Cache;
using Booket.Modules.UserManagement.Infrastructure.Configuration.DataAccess;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Domain;
using Booket.Modules.UserManagement.Infrastructure.Configuration.EventsBus;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Logging;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Mediation;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Processing;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Processing.Outbox;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Quartz;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Security;
using Booket.Modules.UserManagement.Infrastructure.Configuration.Sms;
using Serilog;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration
{
    public class UserManagementStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            KavenegarConfiguration kavenegarConfiguration,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            string textEncryptionKey,
            IEventsBus eventsBus,
            ICacheService cacheService,
            long? internalProcessingPoolingInterval = null)
        {
            var moduleLogger = logger.ForContext("Module", "UserManagement");

            ConfigureCompositionRoot(
                connectionString,
                kavenegarConfiguration,
                executionContextAccessor,
                logger,
                textEncryptionKey,
                eventsBus,
                cacheService);

            QuartzStartup.Initialize(moduleLogger, internalProcessingPoolingInterval);

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            KavenegarConfiguration kavenegarConfiguration,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            string textEncryptionKey,
            IEventsBus eventsBus,
            ICacheService cacheService)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "UserManagement")));

            var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new OutboxModule(new BiDictionary<string, Type>()));

            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new SecurityModule(textEncryptionKey));
            containerBuilder.RegisterModule(new SmsModule(kavenegarConfiguration));
            containerBuilder.RegisterModule(new CacheModule(cacheService));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            UserManagementCompositionRoot.SetContainer(_container);
        }
    }
}