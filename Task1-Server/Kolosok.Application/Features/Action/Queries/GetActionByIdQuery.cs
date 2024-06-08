using AutoMapper;
using Kolosok.Application.Contracts.Action;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Action.Queries;

public class GetActionByIdQuery : IRequest<ActionResponse>
{
    public Guid Id { get; set; }
    
    public GetActionByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public void AddSpecification(params ISpecification<Domain.Entities.Action>[] specifications)
    {
        Specifications = specifications;
    }
    public ISpecification<Domain.Entities.Action>[] Specifications { get; private set; }
}

public class GetActionByIdQueryHandler : IRequestHandler<GetActionByIdQuery, ActionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActionByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(GetActionByIdQuery request, CancellationToken cancellationToken)
    {
        var action = await _unitOfWork.ActionRepository.GetByFiltersAsync(request.Specifications, p => p.Id == request.Id);

        if (action is null)
        {
            throw new ActionNotFoundException(request.Id);
        }

        var response = _mapper.Map<ActionResponse>(action);
        return response;
    }
}