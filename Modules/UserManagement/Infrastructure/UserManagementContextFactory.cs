using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Booket.Modules.UserManagement.Infrastructure;

public class UserManagementContextFactory: IDesignTimeDbContextFactory<UserManagementContext>
{
    public UserManagementContext CreateDbContext(string[] args)
    {
        const string connectionString = "Server=DESKTOP-L2V43NK;Database=Booket;User Id=sa;Password=@Mkh@Npm@13960328;Trusted_Connection=True;TrustServerCertificate=True;";
        var optionsBuilder = new DbContextOptionsBuilder<UserManagementContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        return new UserManagementContext(optionsBuilder.Options, loggerFactory);
    }
}