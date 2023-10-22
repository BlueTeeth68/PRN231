using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business_Logic.ExceptionHandler;
using BusinessLogic.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Implement;

public class JwtService : IJwtService
{
    private readonly AppConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public JwtService(AppConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public string CreateAccessToken(Customer customer)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
            new(ClaimTypes.Email, customer.Email)
        };
        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetCurrentUserId()
    {
        var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
        if (httpContextAccessor?.HttpContext?.User.Identity is ClaimsIdentity claimsIdentity &&
            claimsIdentity.Claims.Any() &&
            int.TryParse(claimsIdentity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                out var customerId))
            return customerId;
        throw new NotFoundException();
    }

    public string GetCurrentUserRole()
    {
        var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
        if (httpContextAccessor?.HttpContext?.User.Identity is ClaimsIdentity claimsIdentity &&
            claimsIdentity.Claims.Any())
        {
            var email = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            if (!string.IsNullOrEmpty(email))
            {
                return email.Equals("admin@FUCarRentingSystem.com") ? "Admin" : "Customer";
            }
        }

        throw new NotFoundException();
    }
}