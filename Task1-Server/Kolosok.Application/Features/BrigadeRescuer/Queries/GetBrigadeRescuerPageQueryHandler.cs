using AutoMapper;
using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.BrigadeRescuer.Queries;

public class GetBrigadeRescuerPageQueryHandler : IRequestHandler<GetBrigadeRescuerPageQuery, IEnumerable<BrigadeRescuerResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBrigadeRescuerPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<BrigadeRescuerResponse>> Handle(GetBrigadeRescuerPageQuery request, CancellationToken cancellationToken)
    {
        var patients = await _unitOfWork.BrigadeRescuerRepository
            .GetAllByFiltersAsync(
                request.Filters,
                request.Specifications
            );
        var response = _mapper.Map<IEnumerable<BrigadeRescuerResponse>>(patients);
        return response;
    }
}