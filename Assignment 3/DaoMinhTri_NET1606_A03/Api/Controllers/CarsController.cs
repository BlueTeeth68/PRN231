using BusinessLogic.Dto.Request.Cars;
using BusinessLogic.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class CarsController : ODataController
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [EnableQuery]
    [HttpGet]
    public ActionResult<IQueryable<CarInformation>> Get()
    {
        return Ok(_carService.GetAllAsync());
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CarInformation>> AddAsync([FromBody] CreateCarDto dto)
    {
        return Ok(await _carService.CreateCarAsync(dto));
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult<CarInformation>> UpdateAsync([FromRoute] int id, [FromBody] UpdateCarDto dto)
    {
        return Ok(await _carService.UpdateAsync(id, dto));
    }
    
    //Delete car
}