using Kolosok.Application.Features.User.Commands;
using Kolosok.Application.Features.User.Queries;
using Kolosok.Application.Features.Victim.Commands;
using Kolosok.Application.Features.Victim.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Infrastructure.Specifications;
using Kolosok.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<BrigadeRescuersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<BrigadeRescuersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] SearchFilter filter)
        {
            var specification = new GetUserFullInformationSpecification();
            var query = new GetUsersPageQuery(filter);
            query.AddSpecification(specification);
            var victims = await _mediator.Send(query);
            return Ok(victims);
        }

        //[HttpGet("{id:guid}")]
        //public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id)
        //{
        //    var specification = new GetVictimFullInformationSpecification();
        //    var query = new GetVictimByIdQuery(id);
        //    query.AddSpecification(specification);
        //    var victim = await _mediator.Send(query);
        //    return Ok(victim);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateVictimCommand request)
        //{
        //    var victim = await _mediator.Send(request);
        //    return Ok(victim);
        //}

        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> Update([FromRoute] Guid id,
        //    [FromBody] UpdateVictimRequestCommand updateBrigadeRescuerRequestCommand)
        //{
        //    updateBrigadeRescuerRequestCommand.Id = id;
        //    var _ = await _mediator.Send(updateBrigadeRescuerRequestCommand);
        //    return Ok();
        //}

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteBrigadeRescuerCommand = new DeleteUserCommand(id);
            await _mediator.Send(deleteBrigadeRescuerCommand);
            return NoContent();
        }
    }
}
