using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBudgetManager.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        return Ok("Only Admins can see this!");
    }
}