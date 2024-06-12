using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Victim.Commands;

public class UpdateVictimRequestCommand: IRequest<Unit>
{
    public ISpecification<Domain.Entities.Victim>[]? Specifications { get; private set; }

    [JsonIgnore] public Guid Id { get; set; }
    
    public ContactRequest Contact { get; set; }

    public UpdateVictimRequestCommand(Guid id)
    {
        Id = id;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.Victim>[] specifications)
    {
        Specifications = specifications;
    }
}

public class UpdateVictimRequestCommandHandler : IRequestHandler<UpdateVictimRequestCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateVictimRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateVictimRequestCommand request, CancellationToken cancellationToken)
    {
        var victim = await _unitOfWork.VictimRepository.GetByFiltersAsync(
            request.Specifications,
            d => d.Id == request.Id);

        if (victim is null)
        {
            throw new VictimNotFoundException(request.Id);
        }

        _mapper.Map(request, victim);
        _unitOfWork.VictimRepository.Update(victim);
        _unitOfWork.ContactRepository.Update(victim.Contact);

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class UpdateVictimRequestCommandValidator : AbstractValidator<UpdateVictimRequestCommand>
{
    public UpdateVictimRequestCommandValidator()
    {
        
    }
}