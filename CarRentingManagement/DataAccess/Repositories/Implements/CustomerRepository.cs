using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implements;

public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
{
    public CustomerRepository(AppDbContext context, ILogger<BaseRepository<Customer>> logger) : base(context, logger)
    {
    }
}