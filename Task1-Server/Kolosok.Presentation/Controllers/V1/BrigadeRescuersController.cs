using Kolosok.Application.Features.BrigadeRescuer.Commands;
using Kolosok.Application.Features.BrigadeRescuer.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Infrastructure.Specifications;
using Kolosok.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class BrigadeRescuersController : ControllerBase
{
    private readonly ILogger<BrigadeRescuersController> _logger;
    private readonly IMediator _mediator;
    private readonly QRCodeService _qrCodeService;

    public BrigadeRescuersController(ILogger<BrigadeRescuersController> logger, IMediator mediator, QRCodeService qrCodeService)
    {
        _logger = logger;
        _mediator = mediator;
        _qrCodeService = qrCodeService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] SearchFilter filter)
    {
        var specification = new GetBrigadeRescuerFullInformationSpecification();
        var query = new GetBrigadeRescuerPageQuery(filter);
        query.AddSpecification(specification);
        var brigadeRescuers = await _mediator.Send(query);
        return Ok(brigadeRescuers);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrigadeRescuerCommand createBrigadeRescuer)
    {
        var newBrigadeRescuer = await _mediator.Send(createBrigadeRescuer);
        return Ok(newBrigadeRescuer);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var specification = new GetBrigadeRescuerFullInformationSpecification();
        var query = new GetBrigadeRescuerByIdQuery(id);
        query.AddSpecification(specification);
        var brigadeRescuer = await _mediator.Send(query);
        return Ok(brigadeRescuer);
    }
    
    [HttpGet("{id:guid}/qrcode")]
    public async Task<ActionResult> GetQRCodeById(Guid id)
    {
        var specification = new GetBrigadeRescuerQrInformationSpecification();
        var query = new GetBrigadeRescuerByIdQuery(id);
        query.AddSpecification(specification);
        var brigadeRescuer = await _mediator.Send(query);
        var qrCodeBytes = await _qrCodeService.GenerateQRCode(brigadeRescuer.Contact);
        return File(qrCodeBytes, "image/png");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateBrigadeRescuerRequestCommand updateBrigadeRescuerRequestCommand)
    {
        updateBrigadeRescuerRequestCommand.Id = id;
        var _ = await _mediator.Send(updateBrigadeRescuerRequestCommand);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleteBrigadeRescuerCommand = new DeleteBrigadeRescuerCommand(id);
        await _mediator.Send(deleteBrigadeRescuerCommand);
        return NoContent();
    }
}