using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Response;
using DataAccess.Models;

namespace BusinessLogic.Interface
{
    public interface ICustomerService
    {
        Task<LoginCustomerDto> LoginAsync(CredentialDto request);
        Task<LoginCustomerDto> RegisterAsync(CreateCustomerDto dto);
        Task<IQueryable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
    }
}
