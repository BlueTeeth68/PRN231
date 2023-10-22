using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Response;
using DataAccess.Models;

namespace BusinessLogic.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(Customer entity)
        {
            return new CustomerDto
            {
                CustomerId = entity.CustomerId,
                CustomerBirthday = entity.CustomerBirthday,
                CustomerName = entity.CustomerName,
                CustomerStatus = entity.CustomerStatus,
                Email = entity.Email,
                Telephone = entity.Telephone
            };
        }

        public static Customer ToEntity(CreateCustomerDto dto)
        {
            return new Customer
            {
                CustomerBirthday = dto.CustomerBirthday,
                CustomerName = dto.CustomerName ?? dto.Email,
                Email = dto.Email,
                Password = dto.Password,
                CustomerStatus = 1,
                Telephone = dto.Telephone
            };
        }

        public static LoginCustomerDto ToLoginDto(Customer entity)
        {
            return new LoginCustomerDto
            {
                CustomerId = entity.CustomerId,
                CustomerBirthday = entity.CustomerBirthday,
                Telephone = entity.Telephone,
                CustomerName = entity.CustomerName,
                Email = entity.Email,
                Token = ""
            };
        }
    }
}