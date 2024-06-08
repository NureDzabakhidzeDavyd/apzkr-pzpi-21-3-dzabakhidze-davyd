using AutoMapper;
using Kolosok.Application.Contracts.Victim;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.Victim.Queries;

public class GetVictimsPageQuery: IRequest<IEnumerable<VictimResponse>>
{
    public ISpecification<Domain.Entities.Victim>[] Specifications { get; private set; }

    public SearchFilter Filters { get; set; }

    public GetVictimsPageQuery(SearchFilter filter)
    {
        Filters = filter;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.Victim>[] specifications)
    {
        Specifications = specifications;
    }
}

public class GetVictimsPageQueryHandler : IRequestHandler<GetVictimsPageQuery, IEnumerable<VictimResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVictimsPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<VictimResponse>> Handle(GetVictimsPageQuery request, CancellationToken cancellationToken)
    {
        var patients = await _unitOfWork.VictimRepository
            .GetAllByFiltersAsync(
                request.Filters,
                request.Specifications
            );
        var response = _mapper.Map<IEnumerable<VictimResponse>>(patients);
        return response;
    }
}