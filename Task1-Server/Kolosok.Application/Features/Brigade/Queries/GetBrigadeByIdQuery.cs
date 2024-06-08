using AutoMapper;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Brigade.Queries;

public class GetBrigadeByIdQuery : IRequest<BrigadeResponse>
{
    public Guid Id { get; set; }
    
    public GetBrigadeByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public void AddSpecification(params ISpecification<Domain.Entities.Brigade>[] specifications)
    {
        Specifications = specifications;
    }
    public ISpecification<Domain.Entities.Brigade>[] Specifications { get; private set; }
}

public class GetBrigadeByIdQueryHandler : IRequestHandler<GetBrigadeByIdQuery, BrigadeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBrigadeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BrigadeResponse> Handle(GetBrigadeByIdQuery request, CancellationToken cancellationToken)
    {
        var brigade = await _unitOfWork.BrigadeRepository.GetByFiltersAsync(request.Specifications, p => p.Id == request.Id);
        
        if (brigade is null)
        {
            throw new BrigadeNotFoundException(request.Id);
        }
        
        var response = _mapper.Map<BrigadeResponse>(brigade);
        return response;
    }
}