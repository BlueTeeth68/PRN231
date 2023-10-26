using Api;
using Api.Middlewares;
using BusinessLogic;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

//Add dependency injection
var configuration = builder.Configuration.Get<AppConfiguration>();
if (configuration != null)
{
    configuration.JwtKey = builder.Configuration["JwtSettings:Key"];
    configuration.Issuer = builder.Configuration["JwtSettings:Issuer"];
    configuration.Audience = builder.Configuration["JwtSettings:Audience"];
    configuration.AdminEmail = builder.Configuration["Admin:Email"];
    configuration.AdminPassword = builder.Configuration["Admin:Password"];
    builder.Services.AddApiConfiguration(configuration.JwtKey, configuration.Issuer, configuration.Audience);
    builder.Services.AddDependency();
    builder.Services.AddSingleton(configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => { o.SwaggerEndpoint("/swagger/v1/swagger.json", "Fu Renting system V1"); });
}

app.UseCors("_publicPolicy");

app.UseAuthentication();

app.UseODataRouteDebug();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();


app.Run();