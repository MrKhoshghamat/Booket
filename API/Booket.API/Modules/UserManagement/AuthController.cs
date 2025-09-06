using Booket.API.Configuration.Authorization;
using Booket.Modules.UserManagement.Application.Contracts;
using Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUser;
using Booket.Modules.UserManagement.Application.UserAuthentications.GetRegisteredUsers;
using Booket.Modules.UserManagement.Application.UserAuthentications.RegisterUser;
using Booket.Modules.UserManagement.Application.UserAuthentications.SendOtp_Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace Booket.API.Modules.UserManagement;

[Route("userManagement/[controller]/[action]")]
[ApiController]
public class AuthController(
    IUserManagementModule userManagementModule) : ControllerBase
{
    [NoPermissionRequired]
    [HttpGet("{phoneNumber}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestOtp(string phoneNumber)
    {
        var otp = await userManagementModule.ExecuteCommandAsync(new SendOtpCommandTest(phoneNumber));
        return Ok(ApiResponse<string>.Ok(otp, "OTP has been sent successfully."));
    }

    [NoPermissionRequired]
    [HttpGet("{phoneNumber}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterNewUser(string phoneNumber)
    {
        var userId = await userManagementModule.ExecuteCommandAsync(new RegisterUserCommand(
            phoneNumber));
        return Ok(ApiResponse<Guid>.Ok(userId, "User has been registered successfully."));
    }

    [HasPermission(AuthenticationPermissions.GetRegisteredUsers)]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<UserRegistrationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserRegistrationDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRegisteredUsers()
    {
        var users = await userManagementModule.ExecuteQueryAsync(new GetRegisteredUsersQuery());
        return Ok(ApiResponse<List<UserRegistrationDto>>.Ok(users, "Registered users retrieved successfully."));
    }
}