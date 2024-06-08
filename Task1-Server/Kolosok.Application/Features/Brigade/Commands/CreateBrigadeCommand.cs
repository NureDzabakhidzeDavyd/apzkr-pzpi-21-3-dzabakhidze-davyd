using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Brigade.Commands;

public class CreateBrigadeCommand : IRequest<BrigadeResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
        
    public IList<Guid> BrigadeRescuers { get; set; }
}

public class CreateBrigadeCommandHandler : IRequestHandler<CreateBrigadeCommand, BrigadeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBrigadeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BrigadeResponse> Handle(CreateBrigadeCommand request, CancellationToken cancellationToken)
    {
        var brigade = _mapper.Map<Domain.Entities.Brigade>(request);
        var newBrigade = await _unitOfWork.BrigadeRepository.CreateAsync(brigade);
        
        if (request.BrigadeRescuers.Any())
        {
            foreach (var rescuerId in request.BrigadeRescuers)
            {
                if(!await _unitOfWork.BrigadeRescuerRepository.ExistAsync(b => b.Id == rescuerId))
                {
                    throw new BrigadeRescuerNotFoundException(rescuerId);
                }

                await _unitOfWork.BrigadeRescuerRepository.UpdatePropertiesAsync(b => b.Id == rescuerId,
                    (b => b.BrigadeId, newBrigade.Id));
            }
        }
        
        await _unitOfWork.SaveChangesAsync();
        var response = _mapper.Map<BrigadeResponse>(newBrigade);
        return response;
    }
}

public class CreateBrigadeCommandValidator : AbstractValidator<CreateBrigadeCommand>
{
    public CreateBrigadeCommandValidator()
    {
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