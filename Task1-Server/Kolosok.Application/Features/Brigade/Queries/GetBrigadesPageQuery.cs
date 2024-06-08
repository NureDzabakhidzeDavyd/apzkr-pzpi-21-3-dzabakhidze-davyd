using AutoMapper;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.Brigade.Queries;

public class GetBrigadesPageQuery : IRequest<IEnumerable<BrigadeResponse>>
{
    public ISpecification<Domain.Entities.Brigade>[] Specifications { get; private set; }

    public SearchFilter Filters { get; set; }

    public GetBrigadesPageQuery(SearchFilter filter)
    {
        Filters = filter;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.Brigade>[] specifications)
    {
        Specifications = specifications;
    }
}

public class GetBrigadesPageQueryHandler : IRequestHandler<GetBrigadesPageQuery, IEnumerable<BrigadeResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBrigadesPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<BrigadeResponse>> Handle(GetBrigadesPageQuery request, CancellationToken cancellationToken)
    {
        var brigades = await _unitOfWork.BrigadeRepository
            .GetAllByFiltersAsync(
                request.Filters,
                request.Specifications
            );
        var response = _mapper.Map<IEnumerable<BrigadeResponse>>(brigades);
        return response;
    }
}