using Booket.BuildingBlocks.Domain;

namespace Booket.Modules.UserManagement.Domain.Users;

public class UserRole(string value)
    : ValueObject
{
    public static UserRole Member => new UserRole(nameof(Member));
    public static UserRole Publisher => new UserRole(nameof(Publisher));
    public static UserRole Admin => new UserRole(nameof(Admin));

    public string Value { get; } = value;
}