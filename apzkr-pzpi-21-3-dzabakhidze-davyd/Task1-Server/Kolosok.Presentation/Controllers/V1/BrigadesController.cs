using Kolosok.Application.Features.Brigade.Commands;
using Kolosok.Application.Features.Brigade.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Infrastructure.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class BrigadesController : ControllerBase
{
    private readonly ILogger<BrigadesController> _logger;
    private readonly IMediator _mediator;

    public BrigadesController(ILogger<BrigadesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] SearchFilter filter)
    {
        var specification = new GetBrigadeFullInformationSpecification();
        var query = new GetBrigadesPageQuery(filter);
        query.AddSpecification(specification);
        var brigades = await _mediator.Send(query);
        return Ok(brigades);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var specification = new GetBrigadeFullInformationSpecification();
        var query = new GetBrigadeByIdQuery(id);
        query.AddSpecification(specification);
        var brigade = await _mediator.Send(query);
        return Ok(brigade);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateBrigadeCommand updateBrigadeRequestCommand)
    {
        updateBrigadeRequestCommand.Id = id;
        updateBrigadeRequestCommand.AddSpecification(new GetBrigadeFullInformationSpecification());
        var _ = await _mediator.Send(updateBrigadeRequestCommand);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrigadeCommand createBrigadeCommand)
    {
        var newBrigade = await _mediator.Send(createBrigadeCommand);
        return Ok(newBrigade);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleteBrigadeCommand = new DeleteBrigadeCommand(id);
        await _mediator.Send(deleteBrigadeCommand);
        return NoContent();
    }
}