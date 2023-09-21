using BusinessLogic.DTOs.Request.User;
using BusinessLogic.DTOs.Response.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerServices _customerServices;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerServices customerServices, ILogger<CustomerController> logger)
    {
        _customerServices = customerServices;
        _logger = logger;
    }

    [HttpPost("authenticate/register")]
    public async Task<ActionResult<UserResponse>> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var returnCustomer = await _customerServices.RegisterAsync(request);
        if (returnCustomer == null)
        {
            return BadRequest("Something wrong when register account.");
        }

        return Ok(returnCustomer);
    }

    [HttpPost("authenticate/login")]
    public async Task<ActionResult<UserResponse>> LoginAsync([FromBody] LoginUserRequest request)
    {
        var returnCustomer = await _customerServices.LoginAsync(request);
        if (returnCustomer == null)
        {
            return BadRequest("Incorrect email or password.");
        }

        return Ok(returnCustomer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetByIdAsync(int id)
    {
        var user = await _customerServices.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound($"User {id} does not exist.");
        }

        return Ok(user);
    }

    [HttpGet()]
    public async Task<ActionResult<List<UserResponse>>> GetAllAsync([FromQuery] string? name)
    {
        if (name == null || name.Trim().Equals(""))
            return Ok(await _customerServices.GetAllAsync());
        return Ok(await _customerServices.GetByNameAscAsync(name));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateUserRequest request)
    {
        var result = await _customerServices.UpdateAsync(id, request);
        if (result < 0)
        {
            return BadRequest("Error when update user. Recheck information.");
        }

        return Ok("Update success.");
    }

    //Delete
}