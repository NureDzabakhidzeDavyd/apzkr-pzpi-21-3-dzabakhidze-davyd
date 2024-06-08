using Kolosok.Application.Features.Victim.Commands;
using Kolosok.Application.Features.Victim.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Infrastructure.Specifications;
using Kolosok.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class VictimsController : ControllerBase
{
    private readonly ILogger<BrigadeRescuersController> _logger;
    private readonly IMediator _mediator;
    private readonly QRCodeService _qrCodeService;

    public VictimsController(ILogger<BrigadeRescuersController> logger, IMediator mediator, QRCodeService qrCodeService)
    {
        _logger = logger;
        _mediator = mediator;
        _qrCodeService = qrCodeService;
    }

    [HttpGet("{id:guid}/qrcode")]
    public async Task<ActionResult> GetVictimQRCodeById(Guid id)
    {
        var specification = new GetVictimQrInformationSpecification();
        var query = new GetVictimByIdQuery(id);
        query.AddSpecification(specification);
        var victim = await _mediator.Send(query);
        var qrCodeBytes = await _qrCodeService.GenerateQRCode(victim.Contact);
        return File(qrCodeBytes, "image/png");
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] SearchFilter filter)
    {
        var specification = new GetVictimFullInformationSpecification();
        var query = new GetVictimsPageQuery(filter);
        query.AddSpecification(specification);
        var victims = await _mediator.Send(query);
        return Ok(victims);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var specification = new GetVictimFullInformationSpecification();
        var query = new GetVictimByIdQuery(id);
        query.AddSpecification(specification);
        var victim = await _mediator.Send(query);
        return Ok(victim);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVictimCommand request)
    {
        var victim = await _mediator.Send(request);
        return Ok(victim);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateVictimRequestCommand updateBrigadeRescuerRequestCommand)
    {
        updateBrigadeRescuerRequestCommand.Id = id;
        var _ = await _mediator.Send(updateBrigadeRescuerRequestCommand);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleteBrigadeRescuerCommand = new DeleteVictimCommand(id);
        await _mediator.Send(deleteBrigadeRescuerCommand);
        return NoContent();
    }
}