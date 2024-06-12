using AutoMapper;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Contracts.Diagnosis;
using Kolosok.Application.Features.Brigade.Queries;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.Diagnoses.Queries;

public class GetDiagnosesPageQueryHandler : IRequestHandler<GetDiagnosesPageQuery, IEnumerable<DiagnosisResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDiagnosesPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<DiagnosisResponse>> Handle(GetDiagnosesPageQuery request, CancellationToken cancellationToken)
    {
        var patients = await _unitOfWork.DiagnosisRepository
            .GetAllByFiltersAsync(
                request.Filters,
                request.Specifications
            );
        var response = _mapper.Map<IEnumerable<DiagnosisResponse>>(patients);
        return response;
    }
}