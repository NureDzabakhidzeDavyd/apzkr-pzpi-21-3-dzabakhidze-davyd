using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.Action;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Action.Commands;

public class CreateActionCommand : IRequest<ActionResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ActionTime { get; set; }
    public string ActionType { get; set; }
    public string ActionPlace { get; set; }
    
    public Guid BrigadeRescuerId { get; set; }
    
    public Guid VictimId { get; set; }
}

public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, ActionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateActionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(CreateActionCommand request, CancellationToken cancellationToken)
    {
        var action = _mapper.Map<Domain.Entities.Action>(request);
        
        var victim = await _unitOfWork.VictimRepository.GetByFiltersAsync(null, victim => victim.Id == request.VictimId);
        if (victim is null)
        {
            throw new VictimNotFoundException(request.VictimId);
        }
        
        var brigadeRescuer = await _unitOfWork.BrigadeRescuerRepository.GetByFiltersAsync(null, brigade => brigade.Id == request.BrigadeRescuerId);
        if (brigadeRescuer is null)
        {
            throw new BrigadeRescuerNotFoundException(request.BrigadeRescuerId);
        }

        var newAction = await _unitOfWork.ActionRepository.CreateAsync(action);
        await _unitOfWork.SaveChangesAsync();
        var response = _mapper.Map<ActionResponse>(newAction);
        return response;
    }
}

public class CreateActionCommandValidator : AbstractValidator<CreateActionCommand>
{
    public CreateActionCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.ActionTime).NotEmpty();
        RuleFor(p => p.ActionType).NotEmpty();
        RuleFor(p => p.ActionPlace).NotEmpty();
        RuleFor(p => p.BrigadeRescuerId).NotEmpty();
        RuleFor(p => p.VictimId).NotEmpty();
    }
}