using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Request.Customers;
using BusinessLogic.Dto.Response;
using BusinessLogic.Dto.Response.Customers;

namespace BusinessLogic.Interface
{
    public interface ICustomerService
    {
        Task<LoginCustomerDto> LoginAsync(CredentialDto request);
        Task<LoginCustomerDto> RegisterAsync(CreateCustomerDto dto);
        Task<IQueryable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);

        Task<CustomerDto> UpdateProfileAsync(UpdateCustomerDto dto);
        Task<CustomerDto> ChangePasswordAsync(ChangePasswordDto dto);
        Task<CustomerDto> ChangeCustomerStatusAsync(int id);
    }
}
