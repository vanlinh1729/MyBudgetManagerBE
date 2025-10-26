using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManager.Application.Features.UserBalances.Queries.GetUserBalance;

namespace MyBudgetManager.API.Controllers;

[ApiController]
[Authorize]
[Route("api/user-balance")]
public class UserBalanceController : ControllerBase
{
    private readonly ISender _sender;

    public UserBalanceController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetBalance()
    {
       var userBalance =  await _sender.Send(new GetUserBalanceQuery());
       return Ok(userBalance);
    }
}