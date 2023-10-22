using BusinessLogic.Dto.Response;
using BusinessLogic.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}