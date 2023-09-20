using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;

namespace BusinessLogic.Services.Interfaces;

public interface ICustomerServices
{
    Task<LoginUserResponse?> RegisterAsync(RegisterUserRequest request);
    Task<LoginUserResponse?> LoginAsync(LoginUserRequest request);
}