using BusinessLogic.Dto.Request.Renting;
using BusinessLogic.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentingTransactionsController : ODataController
{
    private readonly ITransactionService _transactionService;

    public RentingTransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [EnableQuery]
    [HttpGet]
    public ActionResult<IQueryable<RentingTransaction>> Get()
    {
        return Ok(_transactionService.GetAll());
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RentingTransaction>> CreateAsync([FromBody] List<RentingDetailDto> request)
    {
        return Ok(await _transactionService.CreateTransactionAsync(request));
    }
}