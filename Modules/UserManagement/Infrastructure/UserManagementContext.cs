using Booket.BuildingBlocks.Application.Outbox;
using Booket.BuildingBlocks.Infrastructure.Inbox;
using Booket.BuildingBlocks.Infrastructure.InternalCommands;
using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Booket.Modules.UserManagement.Domain.Users;
using Booket.Modules.UserManagement.Infrastructure.Domain.UserRegistrations;
using Booket.Modules.UserManagement.Infrastructure.Domain.Users;
using Booket.Modules.UserManagement.Infrastructure.Inbox;
using Booket.Modules.UserManagement.Infrastructure.InternalCommands;
using Booket.Modules.UserManagement.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booket.Modules.UserManagement.Infrastructure
{
    public class UserManagementContext(DbContextOptions options, ILoggerFactory loggerFactory)
        : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<InboxMessage> InboxMessages { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        private readonly ILoggerFactory _loggerFactory = loggerFactory;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRegistrationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}