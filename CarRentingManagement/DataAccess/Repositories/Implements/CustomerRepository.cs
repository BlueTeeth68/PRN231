using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
{
    public CustomerRepository(AppDbContext context, ILogger<BaseRepository<Customer>> logger) : base(context, logger)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        var normalizeEmail = email.Trim().ToLower();
        return await _dbSet.FirstOrDefaultAsync(c => c.Email.Trim().ToLower().Equals(normalizeEmail));
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var customer = await GetByEmailAsync(email);
        return customer != null;
    }

    public async Task<Customer?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var customer = await GetByEmailAsync(email);
        if (customer != null)
        {
            if (password.Equals(customer.Password))
                return customer;
        }
        return null;
    }
}