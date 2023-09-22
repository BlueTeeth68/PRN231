using BusinessLogic.DTOs.Response.CarInformation;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SupplierController
{
    private readonly ISupplierService _supplierService;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(ISupplierService supplierService, ILogger<SupplierController> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplierResponse>>> GetAllAsync()
    {
        return await _supplierService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierResponse>> GetByIdAsync(int id)
    {
        return await _supplierService.GetByIdAsync(id);
    }
}