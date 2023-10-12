using Business_Logic.Implement;
using Business_Logic.Interface;
using DataAccess;
using DataAccess.Repositories.Implement;
using DataAccess.Repositories.Interface;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarRentalRepository, CarRentalRepository>();
        services.AddScoped<IProducerRepository, ProducerRepository>();

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<ICarRentalService, CarRentalService>();

        return services;
    }
}