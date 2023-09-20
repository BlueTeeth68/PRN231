using AutoMapper;
using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.Implements;

public class CustomerServices : ICustomerServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerServices> _logger;
    private readonly IMapper _mapper;

    public CustomerServices(IUnitOfWork unitOfWork, ILogger<CustomerServices> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<LoginUserResponse?> RegisterAsync(RegisterUserRequest request)
    {
        if (await _unitOfWork.CustomerRepository.ExistsByEmailAsync(request.Email))
        {
            return null;
        }

        var customerObj = _mapper.Map<Customer>(request);
        await _unitOfWork.CustomerRepository.AddAsync(customerObj);
        var success = await _unitOfWork.SaveChangeAsync();
        if (success > 0)
        {
            var createdCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerObj.CustomerId);
            var result = _mapper.Map<LoginUserResponse>(createdCustomer);
            return result;
        }

        return null;
    }

    public async Task<LoginUserResponse?> LoginAsync(LoginUserRequest request)
    {
        var customerObj =
            await _unitOfWork.CustomerRepository.GetByEmailAndPasswordAsync(request.Email, request.Password);
        if (customerObj == null)
        {
            return null;
        }

        var result = _mapper.Map<LoginUserResponse>(customerObj);
        return result;
    }
}