using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface ICustomerRepository:IBaseRepository<Customer>
{
    Task<Customer?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<Customer?> GetByEmailAndPasswordAsync(string email, string password);
    Task<List<Customer>> GetCustomerByNameAscAsync(string name);
}