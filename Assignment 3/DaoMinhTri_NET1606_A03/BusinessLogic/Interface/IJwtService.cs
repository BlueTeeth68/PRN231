using DataAccess.Models;

namespace BusinessLogic.Interface;

public interface IJwtService
{
    string CreateAccessToken(Customer customer);

    int GetCurrentUserId();

    string GetCurrentUserRole();
}