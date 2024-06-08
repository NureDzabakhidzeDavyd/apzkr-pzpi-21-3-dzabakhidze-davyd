using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Validators;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.BrigadeRescuer.Commands;

public class UpdateBrigadeRescuerRequestCommand : IRequest<Unit>
{
    public ISpecification<Domain.Entities.BrigadeRescuer>[]? Specifications { get; private set; }

    [JsonIgnore] public Guid Id { get; set; }
    
    public ContactRequest Contact { get; set; }
    
    public string Position { get; set; }
    public string Specialization { get; set; }
    
    public Guid BrigadeId { get; set; }
    
    public UpdateBrigadeRescuerRequestCommand(Guid id)
    {
        Id = id;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.BrigadeRescuer>[] specifications)
    {
        Specifications = specifications;
    }
}

public class UpdateBrigadeRescuerRequestCommandHandler : IRequestHandler<UpdateBrigadeRescuerRequestCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBrigadeRescuerRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateBrigadeRescuerRequestCommand request, CancellationToken cancellationToken)
    {
        var brigadeRescuer = await _unitOfWork.BrigadeRescuerRepository.GetByFiltersAsync(
            request.Specifications,
            d => d.Id == request.Id);

        if (brigadeRescuer is null)
        {
            throw new BrigadeRescuerNotFoundException(request.Id);
        }
        
        if (request.BrigadeId != Guid.Empty)
        {
            var brigade = await _unitOfWork.BrigadeRepository.GetByFiltersAsync(null, b => b.Id == request.BrigadeId);
            if (brigade is null)
            {
                throw new BrigadeNotFoundException(request.BrigadeId);
            }
        }

        _mapper.Map(request, brigadeRescuer);
        _unitOfWork.BrigadeRescuerRepository.Update(brigadeRescuer);
        _unitOfWork.ContactRepository.Update(brigadeRescuer.Contact);

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class UpdateBrigadeRescuerRequestCommandValidator : AbstractValidator<UpdateBrigadeRescuerRequestCommand>
{
    public UpdateBrigadeRescuerRequestCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Contact)
            .NotNull().WithMessage("{PropertyName} is required.")
            .SetValidator(new ContactValidator());
        RuleFor(p => p.Position)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(p => p.Specialization)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(p => p.BrigadeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}