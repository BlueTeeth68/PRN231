using BusinessLogic.Mapper;
using BusinessLogic.Services.Implements;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Implements;
using DataAccess.Repositories.Interfaces;
using DataAccess.UnitOfWork.Implements;
using DataAccess.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services, string? connectionString)
    {
        //Add Db context
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString?? ""));

        //Add Repository
        services.AddScoped<ICarInformationRepository, CarInformationRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IRentingDetailRepository, RentingDetailRepository>();
        services.AddScoped<IRentingTransactionRepository, RentingTransactionRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        //Add Services
        services.AddScoped<ICustomerServices, CustomerService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICarInformationService, CarInformationService>();
        services.AddScoped<IManufacturerService, ManufacturerService>();
        services.AddScoped<ISupplierService, SupplierService>();

        //Add Mapper
        services.AddAutoMapper(typeof(UserMappingProfile));
        services.AddAutoMapper(typeof(CarInformationMappingProfile));
        return services;
    }
}