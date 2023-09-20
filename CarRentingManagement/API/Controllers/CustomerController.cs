using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController:ControllerBase
{
    private readonly ICustomerServices _customerServices;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerServices customerServices, ILogger<CustomerController> logger)
    {
        _customerServices = customerServices;
        _logger = logger;
    }

    [HttpPost("authenticate/register")]
    public async Task<ActionResult<LoginUserResponse>> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var returnCustomer = await _customerServices.RegisterAsync(request);
        if (returnCustomer == null)
        {
            return BadRequest("Something wrong when register account.");
        }

        return Ok(returnCustomer);
    }

    [HttpPost("authenticate/login")]
    public async Task<ActionResult<LoginUserResponse>> LoginAsync([FromBody] LoginUserRequest request)
    {
        var returnCustomer = await _customerServices.LoginAsync(request);
        if (returnCustomer == null)
        {
            return BadRequest("Incorrect email or password.");
        }

        return Ok(returnCustomer);
    }
    
}