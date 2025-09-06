using System.Security.Claims;

namespace Booket.Modules.UserManagement.Application.UserAuthentications.Authenticate;

public class UserRegistrationDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public List<Claim> Claims { get; set; }
}