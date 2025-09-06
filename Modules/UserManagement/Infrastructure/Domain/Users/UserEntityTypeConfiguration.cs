using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Booket.Modules.UserManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booket.Modules.UserManagement.Infrastructure.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value))
                .ValueGeneratedNever();

            builder.Property(x => x.UserRegistrationId)
                .HasConversion(
                    id => id.Value,
                    value => new UserRegistrationId(value))
                .ValueGeneratedNever();

            builder.Property<UserRegistrationId>("UserRegistrationId").HasColumnName("UserRegistrationId");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_name").HasColumnName("Name");

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.WithOwner().HasForeignKey("UserId");
                b.ToTable("UserRoles", "users");
                b.Property<UserId>("UserId");
                b.Property<string>("Value").HasColumnName("RoleCode");
                b.HasKey("UserId", "Value");
            });
        }
    }
}
