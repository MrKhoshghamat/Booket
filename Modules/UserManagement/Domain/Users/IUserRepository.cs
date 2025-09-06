namespace Booket.Modules.UserManagement.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);
}