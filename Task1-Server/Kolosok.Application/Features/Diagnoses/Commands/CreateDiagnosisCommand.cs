using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.Diagnosis;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Diagnoses.Commands;

public class CreateDiagnosisCommand : IRequest<DiagnosisResponse>
{
    public string Name { get; set; }
    public string Note { get; set; }
    
    public DateTime DetectionTime { get; set; } = DateTime.Now;
    
    public Guid VictimId { get; set; }
}

public class CreateDiagnosisCommandHandler : IRequestHandler<CreateDiagnosisCommand, DiagnosisResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDiagnosisCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiagnosisResponse> Handle(CreateDiagnosisCommand request, CancellationToken cancellationToken)
    {
        var diagnosis = _mapper.Map<Diagnosis>(request);
        
        var victim =
            await _unitOfWork.VictimRepository.GetByFiltersAsync(null,
                rescuer => rescuer.Id == request.VictimId);
        if (victim is null)
        {
            throw new ContactNotFoundException(request.VictimId);
        }
        
        var newDiagnosis = await _unitOfWork.DiagnosisRepository.CreateAsync(diagnosis);
        await _unitOfWork.SaveChangesAsync();
        var response = _mapper.Map<DiagnosisResponse>(newDiagnosis);
        return response;
    }
}

public class CreateDiagnosisCommandValidator : AbstractValidator<CreateDiagnosisCommand>
{
    public CreateDiagnosisCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.VictimId).NotEmpty();
    }
}