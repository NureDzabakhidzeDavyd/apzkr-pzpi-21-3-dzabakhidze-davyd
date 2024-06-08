using Kolosok.Application.Features.Brigade.Commands;
using Kolosok.Application.Features.Brigade.Queries;
using Kolosok.Application.Features.Diagnoses.Commands;
using Kolosok.Application.Features.Diagnoses.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Infrastructure.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class DiagnosesController : ControllerBase
{
    private readonly ILogger<DiagnosesController> _logger;
    private readonly IMediator _mediator;

    public DiagnosesController(ILogger<DiagnosesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] SearchFilter filter)
    {
        var specification = new GetDiagnosisFullInformationSpecification();
        var query = new GetDiagnosesPageQuery(filter);
        query.AddSpecification(specification);
        var brigades = await _mediator.Send(query);
        return Ok(brigades);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var specification = new GetDiagnosisFullInformationSpecification();
        var query = new GetDiagnosisByIdQuery(id);
        query.AddSpecification(specification);
        var brigade = await _mediator.Send(query);
        return Ok(brigade);
    }
    
    //Obsolete
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateDiagnosisRequestCommand updateDiagnosisRequestCommand)
    {
        var _ = await _mediator.Send(updateDiagnosisRequestCommand);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDiagnosisCommand createBrigadeCommand)
    {
        var diagnosis = await _mediator.Send(createBrigadeCommand);
        return Ok(diagnosis);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleteDiagnosisCommand = new DeleteDiagnosisCommand(id);
        await _mediator.Send(deleteDiagnosisCommand);
        return NoContent();
    }
}