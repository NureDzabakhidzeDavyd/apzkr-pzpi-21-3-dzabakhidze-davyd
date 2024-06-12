using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Brigade.Commands;

public class UpdateBrigadeCommand : IRequest<Unit>
{
    public ISpecification<Domain.Entities.Brigade>[]? Specifications { get; private set; }

    [JsonIgnore] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Guid>? BrigadeRescuers { get; set; }

    public UpdateBrigadeCommand(Guid id)
    {
        Id = id;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.Brigade>[] specifications)
    {
        Specifications = specifications;
    }
}

public class UpdateBrigadeCommandHandler : IRequestHandler<UpdateBrigadeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBrigadeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateBrigadeCommand request, CancellationToken cancellationToken)
    {
        var brigade = await _unitOfWork.BrigadeRepository.GetByFiltersAsync(
            request.Specifications,
            p => p.Id == request.Id);

        if (brigade is null)
        {
            throw new BrigadeNotFoundException(request.Id);
        }

        _mapper.Map(request, brigade);
        _unitOfWork.BrigadeRepository.Update(brigade);

        if (request.BrigadeRescuers != null && request.BrigadeRescuers.Any())
        {
            foreach (var brigadeRescuerId in request.BrigadeRescuers)
            {
                var updateResult = await _unitOfWork.BrigadeRescuerRepository.UpdatePropertiesAsync(
                    b => b.Id == brigadeRescuerId,
                    (b => b.BrigadeId, brigade.Id));

                if (!updateResult) throw new BrigadeRescuerNotFoundException(brigadeRescuerId);
            }
        }

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class UpdateBrigadeCommandValidator : AbstractValidator<UpdateBrigadeCommand>
{
    public UpdateBrigadeCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
    }
}