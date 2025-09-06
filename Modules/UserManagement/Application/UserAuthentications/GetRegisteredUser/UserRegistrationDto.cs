namespace Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUser;

public class UserRegistrationDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegisterDate { get; set; }
    public string StatusCode { get; set; }
}