using BusinessLogic.DTOs.Response.CarInformation;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/manufacturers")]
public class ManufacturerController:ControllerBase
{
    
    private readonly IManufacturerService _manufacturerService;
    private readonly ILogger<ManufacturerController> _logger;

    public ManufacturerController(IManufacturerService manufacturerService, ILogger<ManufacturerController> logger)
    {
        _manufacturerService = manufacturerService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ManufacturerResponse>>> GetAllAsync()
    {
        return await _manufacturerService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManufacturerResponse>> GetByIdAsync(int id)
    {
        return await _manufacturerService.GetByIdAsync(id);
    }
    
}