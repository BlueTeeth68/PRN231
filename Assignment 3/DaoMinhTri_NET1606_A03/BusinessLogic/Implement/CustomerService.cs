﻿using Application;
using Business_Logic.ExceptionHandler;
using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Response;
using BusinessLogic.Interface;
using BusinessLogic.Mapper;
using DataAccess.Models;

namespace BusinessLogic.Implement
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public CustomerService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<LoginCustomerDto> LoginAsync(CredentialDto request)
        {
            var normalizeEmail = request.Email.Trim().ToLower();
            var entity = await _unitOfWork.CustomerRepository.GetAsync(
                filter: c =>
                    c.Email.ToLower().Equals(normalizeEmail) && c.Password != null &&
                    c.Password.Equals(request.Password) && c.CustomerStatus == 1,
                orderBy: null,
                includeProperties: ""
            ).ContinueWith(
                t => t.Result.First() ?? throw new UnauthorizedAccessException("Incorrect email or password"));
            var result = CustomerMapper.ToLoginDto(entity);
            result.Token = _jwtService.CreateAccessToken(entity);
            return result;
        }

        public async Task<LoginCustomerDto> RegisterAsync(CreateCustomerDto dto)
        {
            if (await _unitOfWork.CustomerRepository.ExistByEmailAsync(dto.Email))
                throw new ConflictException($"Email {dto.Email} has been existed.");
            var entity = CustomerMapper.ToEntity(dto);
            entity = await _unitOfWork.CustomerRepository.AddAsync(entity)
                .ContinueWith(t => t.Result ?? throw new BadRequestException("Error when create user."));
            await _unitOfWork.SaveChangeAsync();

            var result = CustomerMapper.ToLoginDto(entity);
            result.Token = _jwtService.CreateAccessToken(entity);
            return result;
        }

        public async Task<IQueryable<CustomerDto>> GetAllAsync()
        {
            //Check role
            return await _unitOfWork.CustomerRepository.GetAllAsync()
                .ContinueWith(t =>
                    t.Result.Select(CustomerMapper.ToDto).AsQueryable());
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            return await _unitOfWork.CustomerRepository.GetByIdAsync(id)
                .ContinueWith(t => t.Result != null
                    ? CustomerMapper.ToDto(t.Result)
                    : throw new NotFoundException($"Customer {id} does not exist."));
        }
    }
}