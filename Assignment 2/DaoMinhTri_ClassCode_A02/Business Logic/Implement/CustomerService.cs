using Business_Logic.Dto.Request;
using Business_Logic.Dto.Response;
using Business_Logic.ExceptionHandler;
using Business_Logic.Interface;
using Business_Logic.Mapper;
using DataAccess.Models;
using DataAccess.Repositories.Interface;

namespace Business_Logic.Implement;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<CustomerDto> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id).ContinueWith(
            t => t.Result != null
                ? CustomerMapper.ToDto(t.Result)
                : throw new NotFoundException($"Customer {id} does not exist."));
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        return await _repo.GetAllAsync().ContinueWith(
            t => t.Result.Select(CustomerMapper.ToDto).ToList());
    }

    public async Task<CustomerDto> CreateAsync(Customer customer)
    {
        return await _repo.AddAsync(customer).ContinueWith(
            t => t.Result != null
                ? CustomerMapper.ToDto(t.Result)
                : throw new BadRequestException("Error when create customer."));
    }

    public async Task<CustomerDto> LoginAsync(string email, string password)
    {
        return await _repo.GetByEmailAndPasswordAsync(email, password)
            .ContinueWith(t =>
                t.Result != null
                    ? CustomerMapper.ToDto(t.Result)
                    : throw new BadRequestException("Incorrect email or password."));
    }

    public async Task<CustomerDto> RegisterAsync(CreateCustomerDto customer)
    {
        if (await _repo.ExistByEmailAsync(customer.Email))
        {
            throw new ConflictException("Customer email was existed.");
        }

        var createCustomer = CustomerMapper.ToEntity(customer);

        return await _repo.AddAsync(createCustomer)
            .ContinueWith(t =>
                t.Result != null ? CustomerMapper.ToDto(t.Result) : throw new BadRequestException("Can not register."));
    }
}