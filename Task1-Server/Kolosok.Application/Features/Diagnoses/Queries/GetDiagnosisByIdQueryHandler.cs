using AutoMapper;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Contracts.Diagnosis;
using Kolosok.Application.Features.Brigade.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Diagnoses.Queries;

public class GetDiagnosisByIdQueryHandler : IRequestHandler<GetDiagnosisByIdQuery, DiagnosisResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDiagnosisByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiagnosisResponse> Handle(GetDiagnosisByIdQuery request, CancellationToken cancellationToken)
    {
        var diagnosis = await _unitOfWork.DiagnosisRepository.GetByFiltersAsync(request.Specifications, p => p.Id == request.Id);
        
        if (diagnosis is null)
        {
            throw new DiagnosisNotFoundException(request.Id);
        }
        
        var response = _mapper.Map<DiagnosisResponse>(diagnosis);
        return response;
    }
}