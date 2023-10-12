using Business_Logic.Dto.Request;
using Business_Logic.Dto.Response;
using DataAccess.Models;

namespace Business_Logic.Mapper;

public static class CustomerMapper
{
    public static CustomerDto ToDto(Customer entity)
    {
        return new CustomerDto
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            Mobile = entity.Mobile,
            Birthday = entity.Birthday,
            IdentityCard = entity.IdentityCard,
            LicenceNumber = entity.LicenceNumber,
            LicenceDate = entity.LicenceDate,
            Email = entity.Email
        };
    }

    public static Customer ToEntity(CreateCustomerDto dto)
    {
        return new Customer
        {
            CustomerName = dto.CustomerName,
            Mobile = dto.Mobile,
            Birthday = dto.Birthday,
            IdentityCard = dto.IdentityCard,
            LicenceNumber = dto.LicenceNumber,
            LicenceDate = dto.LicenceDate,
            Email = dto.Email,
            Password = dto.Password
        };
    }
    
}