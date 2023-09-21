using BusinessLogic.DTOs.Request.Transaction;
using BusinessLogic.DTOs.Response.Transaction;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<RentingResponse>>> GetAllAsync([FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        if (startDate == null || endDate == null)
            return Ok(await _transactionService.GetAllAsync());
        return Ok(await _transactionService.GetRentingTransactionBetweenAsync(startDate.Value, endDate.Value));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RentingResponse>> GetByIdAsync(int id)
    {
        var result = await _transactionService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("history/{customerId}")]
    public async Task<ActionResult<List<RentingResponse>>> GetHistoryAsync(int customerId)
    {
        return Ok(await _transactionService.GetRentingTransactionHistoryAsync(customerId));
    }

    [HttpPost]
    public async Task<ActionResult<RentingResponse>> CreateRentingRequestAsync([FromBody] CreateRentingRequest request)
    {
        var result = await _transactionService.CreateRentingTransactionAsync(request);
        return CreatedAtAction(nameof(GetAllAsync), new { id = result.RentingTransationId }, result);
    }
}