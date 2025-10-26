using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManager.Application.Features.Categories.Commands.CreateCategory;
using MyBudgetManager.Application.Features.Categories.Commands.DeleteCategory;
using MyBudgetManager.Application.Features.Categories.Commands.UpdateCategory;
using MyBudgetManager.Application.Features.Categories.Queries.GetCategories;
using MyBudgetManager.Application.Features.Categories.Queries.GetCategoryById;

namespace MyBudgetManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/categories")]
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

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
    {
        var listCategories = await _mediator.Send(query);
        return Ok(new
        {
            success = true,
            data = listCategories
        });

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
        => Ok(await _mediator.Send(new GetCategoryByIdQuery(id)));
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.Id) return BadRequest("Mismatched ID.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));
        return NoContent();
    }
}