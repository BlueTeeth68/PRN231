using DataAccess.Models;

namespace DataAccess.Repositories.Interface;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<Customer?> GetByEmailAndPasswordAsync(string email, string password);
    Task<bool> ExistByEmailAsync(string email);
    Task<Customer?> GetByEmailAsync(string email);

}