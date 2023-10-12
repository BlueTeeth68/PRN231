using Api;
using DataAccess.Enum;
using DataAccess.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EnumType<CarStatus>();
modelBuilder.EnumType<RentingStatus>();
modelBuilder.EntitySet<Car>("Cars");
modelBuilder.EntitySet<CarProducer>("CarProducers");
modelBuilder.EntitySet<CarRental>("CarRentals");
modelBuilder.EntitySet<Customer>("Customers");
modelBuilder.EntitySet<Review>("Reviews");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependency();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());


app.Run();