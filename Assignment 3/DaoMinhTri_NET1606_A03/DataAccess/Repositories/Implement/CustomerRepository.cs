using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implement;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var normalizeEmail = email.Trim().ToLower();

        return await _dbSet.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(normalizeEmail) &&
                                                     c.Password.Equals(password));
    }

    public async Task<bool> ExistByEmailAsync(string email)
    {
        return await GetByEmailAsync(email) != null;
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        var normalizeEmail = email.Trim().ToLower();
        return await _dbSet.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(normalizeEmail));
    }
}