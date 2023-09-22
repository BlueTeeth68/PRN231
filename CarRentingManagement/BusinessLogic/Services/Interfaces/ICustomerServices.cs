using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;

namespace BusinessLogic.Services.Interfaces;

public interface ICustomerServices
{
    Task<UserResponse?> RegisterAsync(RegisterUserRequest request);
    Task<UserResponse?> LoginAsync(LoginUserRequest request);
    Task<UserResponse?> GetByIdAsync(int id);
    Task<List<UserResponse>> GetAllAsync();
    Task<bool> UpdateAsync(int id, UpdateUserRequest request);
    Task<bool> ChangePasswordAsync(int id, UpdatePasswordRequest request);
    Task<List<UserResponse>> GetByNameAscAsync(string name);
}