using Kolosok.Application.Features.Action.Commands;
using Kolosok.Application.Features.Action.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Infrastructure.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class ActionsController  : ControllerBase
{
    private readonly ILogger<BrigadesController> _logger;
    private readonly IMediator _mediator;

    public ActionsController(ILogger<BrigadesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] SearchFilter filter)
    {
        var specification = new GetActionFullInformationSpecification();
        var query = new GetActionsPageQuery(filter);
        query.AddSpecification(specification);
        var actions = await _mediator.Send(query);
        return Ok(actions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var specification = new GetActionFullInformationSpecification();
        var query = new GetActionByIdQuery(id);
        query.AddSpecification(specification);
        var action = await _mediator.Send(query);
        return Ok(action);
    }
    
    //Obsolete
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] CreateActionCommand updateBrigadeRequestCommand)
    {
        var _ = await _mediator.Send(updateBrigadeRequestCommand);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateActionCommand createBrigadeCommand)
    {
        var newAction = await _mediator.Send(createBrigadeCommand);
        return Ok(newAction);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new DeleteActionCommand(id);
        await _mediator.Send(request);
        return NoContent();
    }
}