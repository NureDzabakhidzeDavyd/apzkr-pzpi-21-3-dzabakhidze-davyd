using AutoMapper;
using Kolosok.Application.Contracts.Action;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.Action.Queries;

public class GetActionsPageQuery : IRequest<IEnumerable<ActionResponse>>
{
    public ISpecification<Domain.Entities.Action>[] Specifications { get; private set; }

    public SearchFilter Filters { get; set; }

    public GetActionsPageQuery(SearchFilter filter)
    {
        Filters = filter;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.Action>[] specifications)
    {
        Specifications = specifications;
    }
}

public class GetActionsPageQueryHandler : IRequestHandler<GetActionsPageQuery, IEnumerable<ActionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActionsPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ActionResponse>> Handle(GetActionsPageQuery request, CancellationToken cancellationToken)
    {
        var actions = await _unitOfWork.ActionRepository
            .GetAllByFiltersAsync(
                request.Filters,
                request.Specifications
            );
        var response = _mapper.Map<IEnumerable<ActionResponse>>(actions);
        return response;
    }
}