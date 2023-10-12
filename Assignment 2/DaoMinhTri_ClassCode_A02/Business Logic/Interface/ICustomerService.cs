using Business_Logic.Dto.Request;
using Business_Logic.Dto.Response;
using DataAccess.Models;

namespace Business_Logic.Interface;

public interface ICustomerService
{
    Task<CustomerDto> GetByIdAsync(int id);
    Task<List<CustomerDto>> GetAllAsync();
    Task<CustomerDto> CreateAsync(Customer customer);
    Task<CustomerDto> LoginAsync(string email, string password);
    Task<CustomerDto> RegisterAsync(CreateCustomerDto customer);
}