using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Response;

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
