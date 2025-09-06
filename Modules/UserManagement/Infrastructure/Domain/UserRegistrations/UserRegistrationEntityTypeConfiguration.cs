using Booket.Modules.UserManagement.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booket.Modules.UserManagement.Infrastructure.Domain.UserRegistrations
{
    internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("UserRegistrations", "registrations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new UserRegistrationId(value))
                .ValueGeneratedNever();

            builder.Property<string>("PhoneNumber").HasColumnName("PhoneNumber");
            builder.Property<DateTime>("_registerDate").HasColumnName("RegisterDate");

            builder.OwnsOne<UserRegistrationStatus>("_status", b =>
                {
                    b.Property(x => x.Value).HasColumnName("StatusCode");
                });
        }
    }
}
