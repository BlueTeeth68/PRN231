using BusinessLogic.DTOs.Request.CarInformation;
using BusinessLogic.DTOs.Response.CarInformation;
using BusinessLogic.ErrorHandlers;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/car-informations")]
public class CarInformationController : ControllerBase
{
    private readonly ICarInformationService _carInformationService;
    private readonly ILogger<CarInformationController> _logger;

    public CarInformationController(ICarInformationService carInformationService,
        ILogger<CarInformationController> logger)
    {
        _carInformationService = carInformationService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarInformationResponse>> GetByIdAsync(int id)
    {
        var car = await _carInformationService.GetByIdAsync(id);
        if (car == null)
        {
            throw new NotFoundException($"Car {id} does not exist.");
        }

        return Ok(car);
    }

    [HttpGet]
    public async Task<ActionResult<List<CarInformationResponse>>> SearchByNameAsync([FromQuery] string? name)
    {
        if (name == null || name.Trim() == "")
            return Ok(await _carInformationService.GetAllAsync());
        return Ok(await _carInformationService.SearchByNameAsync(name));
    }

    [HttpPost]
    public async Task<ActionResult<CarInformationResponse>> CreateCarAsync(
        [FromBody] CreateCarInformationRequest request)
    {
        var car = await _carInformationService.CreateNewAsync(request);
        if (car == null)
        {
            throw new BadRequestException("Can not create this car information.");
        }

        return Ok(car);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateCarInformationRequest request)
    {
        var success = await _carInformationService.UpdateAsync(id, request);
        if (success)
        {
            return Ok("Update success.");
        }

        throw new BadRequestException("Error when update car.");
    }
}