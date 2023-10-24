using Application;
using DataAccess.Repositories.Implement;
using DataAccess.Repositories.Interface;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Models;
using BusinessLogic.Interface;
using BusinessLogic.Implement;

namespace BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            //Add repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarInformationRepository, CarInformationRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IRentingDetailRepository, RentingDetailRepository>();
            services.AddScoped<IRentingTransactionRepository, RentingTransactionRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            //Add Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}