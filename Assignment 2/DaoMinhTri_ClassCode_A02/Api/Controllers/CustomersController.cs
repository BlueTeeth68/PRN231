using Business_Logic.Dto.Request;
using Business_Logic.Dto.Response;
using Business_Logic.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAsync()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        // GET: api/Customer/5
        [EnableQuery]
        [HttpGet("{id:int}", Name = "Get")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(int id)
        {
            return Ok(await _customerService.GetByIdAsync(id));
        }

        // POST: api/Customer
        [HttpPost("auth/login")]
        public async Task<ActionResult<CustomerDto>> LoginAsync([FromBody] Authorities request)
        {
            return Ok(await _customerService.LoginAsync(request.Email, request.Password));
        }

        [HttpPost("auth/register")]
        public async Task<ActionResult<CustomerDto>> RegisterAsync([FromBody] CreateCustomerDto request)
        {
            return Ok(await _customerService.RegisterAsync(request));
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}