using BusinessLogic.Dto.Request.Customers;
using BusinessLogic.Dto.Response.Customers;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public AuthenticateController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginCustomerDto>> LoginAsync([FromBody] CredentialDto request)
        {
            var result = await _customerService.LoginAsync(request);
            SetHeaders(result.Token);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginCustomerDto>> RegisterAsync([FromBody] CreateCustomerDto request)
        {
            var result = await _customerService.RegisterAsync(request);
            SetHeaders(result.Token);
            return Created(result.CustomerId.ToString(), result);
        }

        private void SetHeaders(string jwt)
        {
            HttpContext.Response.Headers.Append("Authorization", jwt);
        }
    }
}