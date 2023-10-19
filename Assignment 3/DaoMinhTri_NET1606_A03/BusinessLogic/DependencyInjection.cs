using Application;
using DataAccess.Repositories.Implement;
using DataAccess.Repositories.Interface;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Models;

namespace BusinessLogic
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this ServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            //Add repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarInformationRepository, CarInformationRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IRentingDetailRepository, IRentingDetailRepository>();
            services.AddScoped<IRentingTransactionRepository, RentingTransactionRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            return services;
        }

    }
}
