using BusinessLogic.Dto.Request;
using BusinessLogic.Dto.Response;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ODataController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IQueryable<CustomerDto>>> Get()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        [EnableQuery]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetById([FromRoute] int id)
        {
            return Ok(await _customerService.GetByIdAsync(id));
        }

        [HttpPut("account")]
        public async Task<ActionResult<CustomerDto>> Update([FromBody] UpdateCustomerDto dto)
        {
            return Ok(await _customerService.UpdateProfileAsync(dto));
        }

        [HttpPut("account/password")]
        public async Task<ActionResult<CustomerDto>> ChangePassAsync(ChangePasswordDto dto)
        {
            return Ok(await _customerService.ChangePasswordAsync(dto));
        }

        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult<CustomerDto>> ChangeStatusAsync([FromRoute] int id)
        {
            return Ok(await (_customerService.ChangeCustomerStatusAsync(id)));
        }
    }
}