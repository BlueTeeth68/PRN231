using Business_Logic.Dto.Request;
using Business_Logic.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalsController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;

        public CarRentalsController(ICarRentalService carRentalService)
        {
            _carRentalService = carRentalService;
        }

        // GET: api/CarRentals
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<List<CarRental>>> Get()
        {
            return Ok(await _carRentalService.GetAllAsync());
        }

        // POST: api/CarRentals
        [EnableQuery]
        [HttpPost]
        public async Task<ActionResult<CarRental>> Post([FromBody] CreateRentingDto dto)
        {
            return Ok(await _carRentalService.CreateAsync(dto));
        }
    }
}