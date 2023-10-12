using Business_Logic.Dto.Request;
using DataAccess.Models;
using Business_Logic.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    // GET: api/Cars
    [EnableQuery]
    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetAsync()
    {
        return Ok(await _carService.GetAllAsync());
    }

    [EnableQuery]
    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetByIdAsync(int id)
    {
        return Ok(await _carService.GetByIdAsync(id));
    }

    [EnableQuery]
    [HttpPost]
    public async Task<ActionResult<Car>> PostAsync(CreateCarDto car)
    {
        return Ok(await _carService.AddAsync(car));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Car>> UpdateAsync([FromRoute] int id, [FromBody] UpdateCarDto dto)
    {
        return Ok(await _carService.UpdateAsync(id, dto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _carService.DeleteCarAsync(id);
        return Ok();
    }
}