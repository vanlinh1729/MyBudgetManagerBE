using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManager.Application.Features.Categories.Commands.CreateCategory;

namespace MyBudgetManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var categoryDto = await _mediator.Send(command);
        return Ok(categoryDto);
    }
}
