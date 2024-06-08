using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Contracts.Victim;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Validators;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Victim.Commands;

public class CreateVictimCommand : IRequest<VictimResponse>
{
    public ContactRequest Contact { get; set; }
    public Guid BrigadeRescuerId { get; set; }
}

public class CreateVictimCommandHandler : IRequestHandler<CreateVictimCommand, VictimResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateVictimCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<VictimResponse> Handle(CreateVictimCommand request, CancellationToken cancellationToken)
    {
        var brigadeRescuer = await _unitOfWork.BrigadeRescuerRepository.GetByFiltersAsync(null, b => b.Id == request.BrigadeRescuerId);
        if (brigadeRescuer is null)
        {
            throw new BrigadeRescuerNotFoundException(request.BrigadeRescuerId);
        }

        var victim = _mapper.Map<Domain.Entities.Victim>(request);

        var newVictim = await _unitOfWork.VictimRepository.CreateAsync(victim);
        await _unitOfWork.SaveChangesAsync();
        var response = _mapper.Map<VictimResponse>(newVictim);
        response.BrigadeRescuer = _mapper.Map<BrigadeRescuerResponse>(brigadeRescuer);
        return response;
    }
}

public class CreateVictimCommandValidator : AbstractValidator<CreateVictimCommand>
{
    public CreateVictimCommandValidator()
    {
    }
}